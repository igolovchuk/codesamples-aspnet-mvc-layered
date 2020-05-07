using LayeredWebDemo.DAL.Entities;
using System.Linq;

namespace LayeredWebDemo.DAL.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override Message GetById(object id)
        {
            return _objectSet.SingleOrDefault(s => s.MessageId == (int)id);
        }        
    }
}
