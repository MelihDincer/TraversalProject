using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Member.ViewComponents.MemberDashboard
{
    public class _ProfileInformation : ViewComponent
    {
        //private readonly UserManager<AppUser> _userManager;

        //public _ProfileInformation(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
