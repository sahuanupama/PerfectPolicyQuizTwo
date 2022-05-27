using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using PerfectPolicyQuizTwo.Helper;
using PerfectPolicyQuizTwo.Models;
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
        private readonly IApiRequest<Question> _apiQuestionRequest;
        private readonly IApiRequest<Option> _apiOptionRequest;
        private readonly string optionController = "option";

        public OptionController(IApiRequest<Option> apiOptionRequest, IApiRequest<Question> apiQuestionRequest)
        {
            _apiOptionRequest = apiOptionRequest;
            _apiQuestionRequest = apiQuestionRequest;
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

        //Get: OptionController/Create
        public ActionResult Create()
        {
            var question = _apiQuestionRequest.GetAll("Question");
            var questionDropDownListModel = question.Select(c => new SelectListItem
            {
                Text = c.QuestionText,
                Value = c.QuestionId.ToString()
            }).ToList();

            ViewData.Add("questionDDL", questionDropDownListModel);
            return View();
        }

        // POST: QuizController/Create
        [HttpPost]
        public ActionResult Create(Option option)
        {
            try
            {
                Option newOption = new Option()
                {
                    OptionText = option.OptionText,
                    OptionNumber = option.OptionNumber,
                    QuestionId = option.QuestionId
                };
                _apiOptionRequest.Create("option", newOption);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Get: OptionController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                Option option = _apiOptionRequest.GetSingle("option", id);

                return View(option);
            }
            catch
            {
                return View();
            }
        }

        //Post: OptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Option option)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

                _apiOptionRequest.Edit("option", option, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            Option option = _apiOptionRequest.GetSingle("option", id);

            return View(option);
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction(nameof(Index));
                }
                _apiOptionRequest.Delete("option", id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
