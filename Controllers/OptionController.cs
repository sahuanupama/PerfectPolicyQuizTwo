using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Models.QuestionModel;
using PerfectPolicyQuizTwo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class OptionController : Controller
    {
        private readonly IApiRequest<Option> _apiOptionRequest;

        public OptionController(IApiRequest<Option> apiOptionRequest)
        {
            _apiOptionRequest = apiOptionRequest;
        }


        // GET: OptionController
        public ActionResult Index()
        {
            List<Option> options = _apiOptionRequest.GetAll("Option");
            return View(options);
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            Option option = _apiOptionRequest.GetSingle("Option", id);
            return View(option);
        }

        // POST: QuizController/Create
        [HttpPost]
        public ActionResult Create(Option option)
        {
            try
            {
                Option newOption = new Option()
                {
                    OptionId = option.OptionId,
                    OptionText = option.OptionText,
                    OptionNumber = option.OptionNumber
                };
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
