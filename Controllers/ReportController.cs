using ChartJSCore.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuizTwo.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            // TempData["ReportData"] = QuizCount();

            var jsonData = JsonSerializer.Serialize(quizCounts);
            HttpContext.Session.SetString("ReportData", jsonData);


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

        public IActionResult ExportData()
        {
            var jsonData = HttpContext.Session.GetString("ReportData");
            var reportData = JsonSerializer.Deserialize<List<QuizCount>>(jsonData);



            // Get data we need export
            var QuizReport = (List<QuizCount>)TempData.Peek("ReportData");

            //Create an empty memory stream
            var stream = new MemoryStream();

            //Generate  the csv data
            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                // Configuration of the csv writer
                var csv = new CsvWriter(writeFile, CultureInfo.CurrentCulture, leaveOpen: true);

                // write the csv data to the memory stream
                csv.WriteRecord(QuizReport);
            }
            // reset  stream position
            stream.Position = 0;

            // csv MIMe type: text type
            // return the memory stream as a file
            return File(stream, "application/octet-stream", $"ReportDaa_{DateTime.Now.ToString("ddMMMyy_HHmmss")}.csv");

        }
    }
}
