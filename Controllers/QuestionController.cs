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
            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            try
            {
                Question newQuestion = new Question()
                {
                    questionToipc = question.questionToipc,
                    questionText = question.questionText,
                    questionImage = question.questionImage,
                    quizId = question.quizId
                };
                _apiQuestionRequest.Create("Question", newQuestion);

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
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                Question question = _apiQuestionRequest.GetSingle(questionController, id);
                return View(question);
            }
            catch
            {
                return View();
            }
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Question question)
        {
            try
            {
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

                _apiQuestionRequest.Edit(questionController, question, id);

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

            Question question = _apiQuestionRequest.GetSingle(questionController, id);
            return View(question);
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

                _apiQuestionRequest.Delete(questionController, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult QuestionsForQuiz(int id)
        {
            List<Question> quizQuestions = _apiQuestionRequest.GetAllForParentId("Question", "QuestionsForQuizId", id);
            return View("Index", quizQuestions);
        }
    }
}
