using LayeredWebDemo.BLL.Common;
using LayeredWebDemo.DAL.Entities;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredWebDemo.BLL.Hubs
{
    public class ChatHub : Hub
    {
        //public void Send(string name, string message, string toid)
        //{
        //    //var customer = new Maatey.DataAccess.Entities.ApplicationDbContext().Customers.FirstOrDefault(u => u.User.UserName == name);
        //    //var image = "/Content/defaultlayout/images/icons/avatar.jpg";
        //    //if (customer != null)
        //    //{
        //    //    if (!String.IsNullOrEmpty(customer.CompanyLogo))
        //    //    {
        //    //        image = String.Format("data:image/png;base64,{0}", customer.CompanyLogo);
        //    //    }
        //    //}
        //   // var postDate = DateTime.Now.ToString("hh:mm:ss");
        //    // Call the addNewMessageToPage method to update clients.
        //    Clients.All.addNewMessageToPage(name, message);

        //}
        #region Data Members

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Methods

        public void Connect(string userName, string image)
        {
            var id = Context.ConnectionId;


            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName, Image = image });
                //using (var db = new ApplicationDbContext())
                //{
                //    db.ChatUsers.Add(new ChatUser { ConnectionId = id, UserName = userName, Image = image });
                //}
                //var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == id);
                //List<ChatHistory> chathistory = new ApplicationDbContext().ChatHistory.Where(h => h.FromUserName == fromUser.UserName).ToList();

                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, image);


            }

        }

        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            //AddMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }

        public void SendPrivateMessage(string toUserId, string message, string image)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                AddMessageinCache(toUserId, fromUser.UserName, toUser.UserName, message, image);
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, image);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, image);
            }

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCalled);
        }

        //--------------------Who is typing------------------------------------//
        public void IsTyping(string windowId, string fromUserName)
        {
            // do stuff with the html
            SayWhoIsTyping(windowId, fromUserName); //call the function to send the html to the other clients
        }

        public void SayWhoIsTyping(string windowId, string fromUserName)
        {
            // send to 
            Clients.Client(windowId).sayWhoIsTyping(fromUserName);
        }
        //-------------------------------------------------------------------//

        //------------------------get user's history-------------------------------------//
        public void GetHistory(string windowId, string toUserName, string fromUserName)
        {

            List<ChatHistory> chathistory = new ApplicationDbContext().ChatHistory.Where(h => h.ToUserName == toUserName && h.FromUserName == fromUserName || h.ToUserName == fromUserName && h.FromUserName == toUserName).ToList();
            // send to 
            //    Clients.Client(windowId).receiveHistory(chathistory);

            // send to caller user
            Clients.Caller.receiveHistory(chathistory, windowId);
        }
        #endregion

        #region private Messages

        private void AddMessageinCache(string windowId, string fromuserName, string touserName, string message, string image)
        {
            // CurrentMessage.Add(new MessageDetail { WindowId = windowId, UserName = fromuserName, Message = message, Image = image });
            using (var db = new ApplicationDbContext())
            {
                db.ChatHistory.Add(new ChatHistory { FromUserName = fromuserName, ToUserName = touserName, Message = message, Image = image });
                db.SaveChanges();

                var chathistory = db.ChatHistory.Where(h => h.ToUserName == touserName && h.FromUserName == fromuserName || h.ToUserName == fromuserName && h.FromUserName == touserName);

                if (chathistory.Count() > 20)
                    db.ChatHistory.Remove(chathistory.First());
                db.SaveChanges();
            }

            //if (CurrentMessage.Count > 100)
            //    CurrentMessage.RemoveAt(0);
        }

        #endregion


    }
}
