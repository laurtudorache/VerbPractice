using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VerbsPracticeApp.Models;
using VerbsPracticeApp.SessionData;

namespace VerbsPracticeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IVerbsRepository verbsRepository;
        private IVerbRandomizer randomizer;

        public HomeController(ILogger<HomeController> logger, IVerbsRepository verbsRepository, IVerbRandomizer verbRandomizer)
        {
            _logger = logger;
            this.verbsRepository = verbsRepository;
            randomizer = verbRandomizer;
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
            }
            var totalCount = verbsRepository.Count(false, true);
            if (sessionData.Indexes.Length == totalCount)
            {
                var finishedModel = new FinishedModel
                {
                    TotalCount = sessionData.TotalCount,
                    SuccessCount = sessionData.SuccessCount
                };
                sessionData = new ProgressData();
                HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
                return View("Finished", finishedModel);
            }

            var nextVerbIndex = randomizer.GetNextIndex(totalCount, sessionData.Indexes);
            VerbData verb = verbsRepository.GetVerbByIndex(nextVerbIndex);
            List<int> newIndexes = new List<int>(sessionData.Indexes);
            newIndexes.Add(nextVerbIndex);
            sessionData.Indexes = newIndexes.ToArray();
            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
            var model = new VerbModel();
            model.SuccessCount = sessionData.SuccessCount;
            model.TotalCount = sessionData.TotalCount;

            model.Infinitive = verb.Infinitief;
            model.English = verb.English;

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
            var verb = verbsRepository.GetVerbByIndex(sessionData.Indexes.Last());

            var imperfectumSingular = model.ImperfectumSingular ?? string.Empty;
            var imperfectumPlural = model.ImperfectumPlural ?? string.Empty;
            var perfectum = model.Perfectum ?? string.Empty;

            if (imperfectumSingular.ToLower().Trim().Equals(verb.ImperfectumSg)
                &&
                imperfectumPlural.ToLower().Trim().Equals(verb.ImperfectumPl)
                &&
                perfectum.ToLower().Trim().Equals(verb.Perfectum)
                )
            {
                sessionData.SuccessCount++;
            }
            else
            {
                var correctionModel = new VerbModel();
                correctionModel.TotalCount = sessionData.TotalCount;
                correctionModel.SuccessCount = sessionData.SuccessCount;
                correctionModel.Infinitive = verb.Infinitief;
                correctionModel.ImperfectumSingular = verb.ImperfectumSg;
                correctionModel.ImperfectumSingularCorrect = imperfectumSingular.ToLower().Trim().Equals(verb.ImperfectumSg);
                correctionModel.ImperfectumPlural = verb.ImperfectumPl;
                correctionModel.ImperfectumPluralCorrect = imperfectumPlural.ToLower().Trim().Equals(verb.ImperfectumPl);
                correctionModel.Perfectum = verb.Perfectum;
                correctionModel.PerfectumCorrect = perfectum.ToLower().Trim().Equals(verb.Perfectum);
                correctionModel.English = verb.English;
                HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
                return View("WrongAnswer", correctionModel);
            }

            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);

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