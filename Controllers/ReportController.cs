using ChartJSCore.Models;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuizTwo.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class ReportController : Controller
    {
        HttpClient _client;
        public ReportController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        public IActionResult QuizCount()
        {
            var response = _client.GetAsync("Report/QuizCountReport").Result;

            List<QuizCount> quizCounts = response.Content.ReadAsAsync<List<QuizCount>>().Result;
            // it define the chart object itself
            Chart chart = new Chart();
            //define the type of chart
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = quizCounts.Select(c => c.QuizName).ToList();

            BarDataset barData = new BarDataset()
            {
                Label = "Question Per Quiz",
                Data = quizCounts.Select(c => (double?)c.QuestionCount).ToList()
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(barData);

            chart.Data = data;
            ViewData["Chart"] = chart;

            return View();
        }
    }
}
