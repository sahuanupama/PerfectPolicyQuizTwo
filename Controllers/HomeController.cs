using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfectPolicyQuizTwo.Models;
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

        /// <summary>
        /// Loads Help page 
        /// </summary>
        /// <returns>Help view</returns>
        public IActionResult Help()
        {
            return View();
        }

        /// <summary>
        /// Loads home page
        /// </summary>
        /// <returns>Index view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads privacy page
        /// </summary>
        /// <returns>Prvacy view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Loads quiz list 
        /// </summary>
        /// <returns>Quiz list view</returns>
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

        /// <summary>
        /// Error Page
        /// </summary>
        /// <returns>Error View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Question list
        /// </summary>
        /// <returns>Question list view</returns>
        public IActionResult Question()
        {
            List<Question> questions = new();
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

        /// <summary>
        /// Option list
        /// </summary>
        /// <returns>Option view</returns>
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
