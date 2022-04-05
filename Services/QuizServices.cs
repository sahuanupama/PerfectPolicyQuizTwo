using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Models.QuizModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Services
{
    public class QuizServices
    {
        private static HttpClient _client;

        private static void ConfigureClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44379/api/");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static List<Quiz> GetAllQuiz()
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.GetAsync("Quiz").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            List<Quiz> quizzes = response.Content.ReadAsAsync<List<Quiz>>().Result;

            return quizzes;
            //var result = response.Content.ReadAsStringAsync().Result;
            //List<Quiz> teacherList = JsonSerializer.Deserialize<List<Quiz>>(result);
        }

        public static void CreateNewQuiz(Quiz quiz)
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.PostAsJsonAsync("Quiz", quiz).Result;

        }

        // Get a single Quiz

        public static Quiz GetSingleTeacher(int id)
        {
            // Requires an ID
            if (_client == null)
            {
                ConfigureClient();
            }

            // Send a Get request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.GetAsync($"Quiz/{id}").Result;
            // handle the response
            var quiz = response.Content.ReadAsAsync<Quiz>().Result;
            // return a quiz
            return quiz;
        }

        // Update a Quiz

        public static void UpdateQuiz(int id, Quiz updatedQuiz)
        {
            // requires an ID and a Quiz object
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.PutAsJsonAsync($"Quiz/{id}", updatedQuiz).Result;
            // Handle the response (check if success) 

        }

        // Delete a Quiz
        public static void DeleteQuiz(int id)
        {
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.DeleteAsync($"Quiz/{id}").Result;
        }


    }
}
