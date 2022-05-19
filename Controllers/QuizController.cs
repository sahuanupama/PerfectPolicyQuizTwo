using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfectPolicyQuizTwo.Helper;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Services;
using System;
using System.Collections.Generic;
using System.IO;
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

        private IWebHostEnvironment _environment;

        public QuizController(IApiRequest<Quiz> apiQuizRequest, IApiRequest<Question> apiQuestionRequest, IWebHostEnvironment environment)

        {
            _apiQuizRequest = apiQuizRequest;
            _apiQuestionRequest = apiQuestionRequest;
            _environment = environment;
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
                Value = c.QuizTitle,
                Text = c.QuizTitle,
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
                    QuizTitle = quiz.QuizTitle,
                    QuizDate = quiz.QuizDate,
                    QuizPersonName = quiz.QuizPersonName,
                    QuizPassNumber = quiz.QuizPassNumber
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
            var filteredList = quizList.Where(c => c.QuizTitle.ToLower().Contains(filterText.ToLower())).ToList();
            // return this list to the index page
            return View("Index", filteredList);
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
