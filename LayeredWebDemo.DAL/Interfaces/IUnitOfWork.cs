using LayeredWebDemo.DAL.Entities;

namespace LayeredWebDemo.DAL.Interfaces
{
    public interface IUnitOfWork
        {
            IRepository<ApplicationUser> Users { get; }
            IRepository<Message> Messages { get; }
            void Save();
            void Dispose();
        }
    
}