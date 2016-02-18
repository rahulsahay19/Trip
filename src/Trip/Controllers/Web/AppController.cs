using Microsoft.AspNet.Mvc;
using WorldTrip.Models;
using WorldTrip.Services;
using WorldTrip.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldTrip.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private ITripRepository _tripRepository;

        public AppController(IMailService mailService, ITripRepository tripRepository)
        {
            _mailService = mailService;
            _tripRepository = tripRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var trips = _tripRepository.GetTrips();
            return View(trips);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.ConfigurationBuilder["AppSettings:EmailAddress"];

                //If Blank email get received from the config file
                if (string.IsNullOrEmpty(email))
                {
                    //Add Object level Error
                    ModelState.AddModelError("","Some Problem with Email Configuration");
                }
                if (_mailService.SendMail(email,
                    email,
                    $"Contact Mail From {contactViewModel.Name} ({contactViewModel.Email})",
                    contactViewModel.Message))
                {
                    //Upon Success, It will just clear the form
                    ModelState.Clear();

                    ViewBag.MailSent = "Mail Sent, Thanks!";
                }
            }
            return View();
        }
    }
}
