using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using LayeredWebDemo.BLL.Config;
using LayeredWebDemo.BLL.Interfaces;
using LayeredWebDemo.BLL.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;

namespace LayeredWebDemo.Web.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUserService>()
                .To<UserService>()
                .WithConstructorArgument("signInManager", c => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>())
                .WithConstructorArgument("userManager", c => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>());
                
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

        }
    }
}