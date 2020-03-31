using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VerbsPractice.Application.Services;
using VerbsPracticeApp.Models;
using VerbsPracticeApp.SessionData;

namespace VerbsPracticeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVerbsQuery verbsQuery;
        private readonly IVerbRandomizer randomizer;

        public HomeController(ILogger<HomeController> logger, INameServiceResolver verbsRepository, IVerbRandomizer verbRandomizer)
        {
            _logger = logger;
            verbsQuery = verbsRepository.GetByName<IVerbsQuery>(QueryKeys.IrregularBasicVerbs);
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
            var totalCount = verbsQuery.Count();
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
            Verb verb = verbsQuery.GetVerbByIndex(nextVerbIndex);
            List<int> newIndexes = new List<int>(sessionData.Indexes)
            {
                nextVerbIndex
            };
            sessionData.Indexes = newIndexes.ToArray();
            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
            var model = new VerbModel
            {
                SuccessCount = sessionData.SuccessCount,
                TotalCount = sessionData.TotalCount,
                Infinitive = verb.Infinitief,
                English = verb.English
            };

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
            var verb = verbsQuery.GetVerbByIndex(sessionData.Indexes.Last());

            var resultModel = new ResultViewModel
            {
                Infinitive = verb.Infinitief,
                English = verb.English,
                ImperfectumSingular = new TenseModel(verb.ImperfectumSg, model.ImperfectumSingular),
                ImperfectumPlural = new TenseModel(verb.ImperfectumPl, model.ImperfectumPlural),
                Perfectum = new TenseModel(verb.Perfectum, model.Perfectum)
            };
            if (resultModel.IsAnswerCorrect)
            {
                sessionData.SuccessCount++;
            }
            resultModel.CorrectCount = sessionData.SuccessCount;

            HttpContext.Session.Set(SessionKeys.UserProgressKey, sessionData);
            return View("ResultView", resultModel);
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