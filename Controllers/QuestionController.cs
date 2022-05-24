using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuizTwo.Helper;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IApiRequest<Question> _apiQuestionRequest;
        private readonly IApiRequest<Quiz> _apiQuizRequest;
        private readonly string questionController = "Question";
        private IWebHostEnvironment _environment;

        public QuestionController(IApiRequest<Question> apiQuestionRequest, IApiRequest<Quiz> apiQuizRequest, IWebHostEnvironment environment)
        {
            _apiQuestionRequest = apiQuestionRequest;
            _apiQuizRequest = apiQuizRequest;
            _environment = environment;
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
                    QuestionTopic = question.QuestionTopic,
                    QuestionText = question.QuestionText,
                    QuestionImage = question.QuestionImage,
                    QuizId = question.QuizId
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

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                // retrive a folder path
                string folderRoot = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads");
                //combine filename and folder path
                string filePath = Path.Combine(folderRoot, file.FileName);
                //save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new { success = true, message = "File Uploaded" });
            }
            catch (Exception e)
            {

                return BadRequest(new { success = false, message = e.Message });
            }
        }
    }
}
