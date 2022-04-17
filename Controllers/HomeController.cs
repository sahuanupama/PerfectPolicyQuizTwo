using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Models.QuestionModel;
using PerfectPolicyQuizTwo.Models.QuizModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            List<Quiz> quizzes = new();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage Response = client.GetAsync("https://localhost:44395/api/Quiz/").Result;
                quizzes = Response.Content.ReadAsAsync<List<Quiz>>().Result;
            }

            if (quizzes == null)
            {
                return View("Error");
            }

            return View(quizzes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Question()
        {
            List<Models.QuestionModel.Question> questions = new();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage Response = client.GetAsync("https://localhost:44395/api/Question").Result;
                questions = Response.Content.ReadAsAsync<List<Question>>().Result;
            }

            if (questions == null)
            {
                return View("Error");
            }
            return View(questions);
        }

        public IActionResult Option()
        {
            List<Option> options = new();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage Response = client.GetAsync("https://localhost:44395/api/Option").Result;
                options = Response.Content.ReadAsAsync<List<Option>>().Result;
            }

            if (options == null)
            {
                return View();
            }

            return View(options);

        }
    }
}
