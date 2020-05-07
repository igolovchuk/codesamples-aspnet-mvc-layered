using System;

namespace LayeredWebDemo.BLL.DTO
{

    public class NotificationDTO
    {
        public string FromImage { get; set; }

        public int MessageId { get; set; }

        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsRead { get; set; }

    }
}
