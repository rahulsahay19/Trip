using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Trip.Services;
using Trip.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trip.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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
