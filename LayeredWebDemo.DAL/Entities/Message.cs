using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayeredWebDemo.DAL.Entities
{

    [Table("Messages")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public string FromUserNameImage { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsRead { get; set; }

    }
}
