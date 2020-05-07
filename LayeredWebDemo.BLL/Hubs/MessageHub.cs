using LayeredWebDemo.BLL.DTO;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LayeredWebDemo.Web.Hubs
{
    public class MessageHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            context.Clients.All.displayStatus();
        }


        public static IEnumerable<NotificationDTO> GetMessages(string username)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [MessageId],[ToUserName],[FromUserName],[FromUserNameImage],[Subject],[Text],[DateSent],[IsRead]
                               FROM [dbo].[Messages] WHERE [ToUserName] = '" + username + "' ORDER BY [DateSent] DESC", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;
                    
                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new NotificationDTO()
                            {
                                MessageId = x.GetInt32(0),
                                ToUserName = x.GetString(1),
                                FromUserName = x.GetString(2),
                                FromImage = x.GetString(3),
                                Subject = x.GetString(4),
                                Text = x.GetString(5),
                                DateSent = x.GetDateTime(6),
                                IsRead = x.GetBoolean(7)
                            }).ToList();



                }
            }
        }
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            Show();
        }
    }
}