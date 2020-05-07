using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LayeredWebDemo.DAL.Entities
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatHistory> ChatHistory { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
