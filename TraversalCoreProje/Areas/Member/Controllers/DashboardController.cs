using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.username = user.Name + " " + user.Surname;
            ViewBag.userimage = user.ImageUrl;
            ViewBag.memberPhone = user.PhoneNumber;
            ViewBag.memberMail = user.Email;
            return View();
        }

        public async Task<IActionResult> MemberDashboard()
        {
            return View();
        }
    }
}
