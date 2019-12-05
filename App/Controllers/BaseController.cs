using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult RedirectToLocal(string returnTo)
        {
            return Redirect(Url.IsLocalUrl(returnTo) ? returnTo : "/");
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}