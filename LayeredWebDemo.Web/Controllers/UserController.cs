using LayeredWebDemo.BLL.Interfaces;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using LayeredWebDemo.BLL.DTO;

namespace LayeredWebDemo.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Edit
        [HttpGet]
        public ActionResult Edit()
        {
            UserDTO user =  _userService.GetUserById(User.Identity.GetUserId());
            return View(user);
        }

        // POST: User/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result =  await _userService.UpdateUserAsync(user);

                if (result.Succeeded)
                {                   
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(user);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

     
        #endregion

    }
}