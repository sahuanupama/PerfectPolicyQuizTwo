using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Models.QuestionModel;
using PerfectPolicyQuizTwo.Models.QuizModel;
using PerfectPolicyQuizTwo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IApiRequest<Question> _apiQuestionRequest;

        private readonly IApiRequest<Quiz> _apiQuizRequest;

        private readonly string questionController = "Question";

        public QuestionController(IApiRequest<Question> apiQuestionRequest, IApiRequest<Quiz> apiQuizRequest)
        {
            _apiQuestionRequest = apiQuestionRequest;
            _apiQuizRequest = apiQuizRequest;
        }


        // GET: QuestionController
        public ActionResult Index()
        {
            List<Question> question = _apiQuestionRequest.GetAll("Question");
            return View(question);
        }

        public ActionResult QuestionForQuiz(int id)
        {
            List<Question> Question = _apiQuestionRequest.GetAllForParentId(questionController, "QuestionForQuizId", id);
            return View("Index", Question);
        }


        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            Question question = _apiQuestionRequest.GetSingle(questionController, id);
            return View(question);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            var question = _apiQuestionRequest.GetAll("Question");

            var QuestionDropDownListModel = question.Select(c => new SelectListItem
            {
                Text = c.questionToipc,
                Value = c.questionId.ToString()
            }).ToList();

            ViewBag.QuestionDropDown = QuestionDropDownListModel;

            ViewData.Add("questionDDL", QuestionDropDownListModel);

            TempData.Add("questionDDL", QuestionDropDownListModel);

            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            try
            {
                question.questionId = 0;

                _apiQuestionRequest.Create("Question", question);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
