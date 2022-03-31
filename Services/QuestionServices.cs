using PerfectPolicyQuizTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PerfectPolicyQuestionTwo.Services
{
    public class QuestionServices
    {
        private static HttpClient _client;

        private static void ConfigureClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44379/api/");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public static List<Question> GetAllQuestion()
        {

            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.GetAsync("Question").Result;
            List<Question> question = response.Content.ReadAsAsync<List<Question>>().Result;

            return question;

        }

        public static void CreateNewQuestion(Question question)
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.PostAsJsonAsync("Question", questions).Result;

        }

        // Get a single TafeClass

        public static Question GetSingleQuestion(int id)
        {
            // Requires an ID
            if (_client == null)
            {
                ConfigureClient();
            }

            // Send a Get request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.GetAsync($"Question/{id}").Result;
            // handle the response
            var questions = response.Content.ReadAsAsync<Question>().Result;
            // return a tafeClass
            return questions;
        }
    }
}