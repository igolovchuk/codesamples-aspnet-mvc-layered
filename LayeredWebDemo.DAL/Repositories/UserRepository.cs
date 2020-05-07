using System.Linq;
using LayeredWebDemo.DAL.Entities;

namespace LayeredWebDemo.DAL.Repositories
{
    public class UserRepository : Repository<ApplicationUser>
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override ApplicationUser GetById(object id)
        {
            return _objectSet.SingleOrDefault(s => s.Id == (string)id);
        }
    }
}
