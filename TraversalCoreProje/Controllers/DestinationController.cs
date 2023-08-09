using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class DestinationController : Controller
    {
        DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {
            var values = destinationManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult DestinationDetails(int id)
        {
            ViewBag.v = id;
            var values = destinationManager.TGetByID(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult DestinationDetails(Destination p)
        {
            //var values = destinationManager.TGetByID(p.DestinationID);
            //destinationManager.TUpdate(values);
            return View();
        }
    }
}