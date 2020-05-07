using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayeredWebDemo.DAL.Entities
{
    [Table("ChatHistory")]
    public class ChatHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public string Message { get; set; }

        public string Image { get; set; }

    }
}
