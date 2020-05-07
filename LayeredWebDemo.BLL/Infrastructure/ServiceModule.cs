using LayeredWebDemo.DAL.Entities;
using LayeredWebDemo.DAL.Interfaces;
using LayeredWebDemo.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;

namespace LayeredWebDemo.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;
        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }
        public override void Load()
        {
            //Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new ApplicationDbContext());
            Bind(typeof(IUserStore<ApplicationUser>)).To(typeof(UserStore<ApplicationUser>));
        }
    }
}
