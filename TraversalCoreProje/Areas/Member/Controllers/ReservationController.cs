using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        //DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        //ReservationManager reservationManager = new ReservationManager(new EfReservationDal());
        //private readonly UserManager<AppUser> _userManager;

        //public ReservationController(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        //DEPENDENCY INJECTION KULLANIMI ile EF BAĞIMLILIĞI ORTADAN KALKTI.
        private readonly IDestinationService _destinationService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(IDestinationService destinationService, IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _destinationService = destinationService;
            _reservationService = reservationService;
            _userManager = userManager;
        }

        //Onaylanan rezervasyonlar
        public async Task<IActionResult> MyCurrentReservation()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByAccepted(user.Id);
            return View(values);
        }
        //Geçmiş rezervasyonlar
        public async Task<IActionResult> MyOldReservation()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByPrevious(user.Id);
            return View(values);
        }
        //Onay Bekleyen rezervasyonlar
        public async Task<IActionResult> MyApprovalReservation()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByWaitApproval(user.Id);
            return View(values);
        }
        [HttpGet]
        public IActionResult NewReservation()
        {
            List<SelectListItem> values = (from x in _destinationService.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.City,
                                               Value = x.DestinationID.ToString()
                                           }).ToList();
            ViewBag.V = values;
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation p)
        {
            p.AppUserId = 2;
            p.Status = "Onay Bekliyor";
            _reservationService.TAdd(p);
            return RedirectToAction("MyCurrentReservation");
        }
    }
}
