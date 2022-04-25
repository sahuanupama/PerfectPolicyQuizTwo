using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfectPolicyQuizTwo.Helper;
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
    public class QuizController : Controller
    {
        private readonly IApiRequest<Quiz> _apiQuizRequest;
        private readonly IApiRequest<Question> _apiQuestionRequest;
        private readonly string quizController = "Quiz";

        public QuizController(IApiRequest<Quiz> apiQuizRequest, IApiRequest<Question> apiQuestionRequest)
        {
            _apiQuizRequest = apiQuizRequest;
            _apiQuestionRequest = apiQuestionRequest;
        }

        [HttpPost]
        public IActionResult Filter(IFormCollection collection)
        {
            var result = collection["quizDDL"].ToString();
            return RedirectToAction("Index", new { filter = result });
        }

        // GET: QuizController
        public ActionResult Index()
        {
            var quizList = _apiQuizRequest.GetAll("Quiz");
            var quizDDL = quizList.Select(c => new SelectListItem
            {
                Value = c.quizTitle,
                Text = c.quizTitle,
            });

            ViewBag.QuizDDL = quizDDL;

            return View(quizList);
        }

        // GET: QuizController/Details/5
        public ActionResult Details(int id)
        {
            Quiz quiz = _apiQuizRequest.GetSingle(quizController, id);
            return View(quiz);
        }

        // GET: QuizController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            try
            {
                Quiz createdQuiz = new Quiz()
                {
                    quizTitle = quiz.quizTitle,
                    quizdate = quiz.quizdate,
                    quizpresonName = quiz.quizpresonName,
                    quizPassNumber = quiz.quizPassNumber
                };

                _apiQuizRequest.Create(quizController, createdQuiz);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                Quiz quiz = _apiQuizRequest.GetSingle(quizController, id);

                return View(quiz);
            }
            catch
            {
                return View();
            }
        }

        // POST: QuizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Quiz quiz)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

                _apiQuizRequest.Edit(quizController, quiz, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            Quiz quiz = _apiQuizRequest.GetSingle(quizController, id);

            return View(quiz);
        }

        // POST: QuizController/Delete/5
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


                _apiQuizRequest.Delete(quizController, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult FilterQuiz(IFormCollection collection)
        {
            // Retrieve filter text
            string filterText = collection["QuizCreator"];
            // retrieve a list of all teachers
            var quizList = _apiQuizRequest.GetAll(quizController);
            // filter that list, return the results to a new list
            var filteredList = quizList.Where(c => c.quizTitle.ToLower().Contains(filterText.ToLower())).ToList();
            // return this list to the index page
            return View("Index", filteredList);
        }
    }
}
