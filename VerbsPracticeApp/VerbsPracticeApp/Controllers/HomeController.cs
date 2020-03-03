using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using VerbsPracticeApp.Models;
using VerbsPracticeApp.SessionData;

namespace VerbsPracticeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ProgressData sessionData;
            if (HttpContext.Session.Keys.Any(k => k == SessionKeys.UserProgressKey))
            {
                sessionData = HttpContext.Session.Get<ProgressData>(SessionKeys.UserProgressKey);
            }
            else
            {
                sessionData = new ProgressData();
                HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
            }

            var model = new VerbModel();
            model.SuccessCount = sessionData.SuccessCount;
            model.TotalCount = sessionData.TotalCount;
            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
            model.Infinitive = Guid.NewGuid().ToString();
            model.English = "English";

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(VerbModel model)
        {
            ProgressData sessionData;
            if (HttpContext.Session.Keys.Any(k => k == SessionKeys.UserProgressKey))
            {
                sessionData = HttpContext.Session.Get<ProgressData>(SessionKeys.UserProgressKey);
            }
            else
            {
                return RedirectToAction("Index");
            }

            sessionData.TotalCount++;
            if (sessionData.TotalCount % 2 == 1)
            {
                sessionData.SuccessCount++;
            }
            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);

            if (sessionData.TotalCount % 2 == 0)
            {
                var correctionModel = new VerbModel();
                correctionModel.TotalCount = sessionData.TotalCount;
                correctionModel.SuccessCount = sessionData.SuccessCount;
                correctionModel.Infinitive = "Inf";
                correctionModel.ImperfectumSingular = "Sg";
                correctionModel.ImperfectumPlural = "Pl";
                correctionModel.Perfectum = "Perfectum";
                correctionModel.English = "English";
                return View("WrongAnswer", correctionModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}