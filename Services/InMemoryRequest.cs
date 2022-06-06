using Microsoft.AspNetCore.Http;
using PerfectPolicyQuizTwo.Models;
using PerfectPolicyQuizTwo.Models.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Services
{
    public class InMemoryRequest<T> : IApiRequest<T> where T : class
    {
        TestDatabase _db;
        public InMemoryRequest(TestDatabase db, IHttpContextAccessor accessor)
        {
            _db = db;
            accessor.HttpContext.Session.SetString("Token", "TestingToken");
        }

        public T Create(string controllerName, T entity)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    var quiz = entity as Quiz;
                    quiz.QuizId = _db.Quizzes.Count == 0 ? 1 : _db.Quizzes.OrderByDescending(c => c.QuizId).FirstOrDefault().QuizId + 1;
                    _db.Quizzes.Add(quiz);
                    return quiz as T;

                case nameof(Question):
                    var question = entity as Question;
                    question.QuestionId = _db.Questions.Count == 0 ? 1 : _db.Questions.OrderByDescending(c => c.QuestionId).FirstOrDefault().QuestionId + 1;
                    _db.Questions.Add(question);
                    return question as T;

                case nameof(Option):
                    var option = entity as Option;
                    option.OptionId = _db.Options.Count == 0 ? 1 : _db.Options.OrderByDescending(c => c.OptionId).FirstOrDefault().OptionId + 1;
                    _db.Options.Add(option);
                    return option as T;

                default:
                    return null;
            }
        }

        public void Delete(string controllerName, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    var quizEntity = _db.Quizzes.Where(c => c.QuizId == id).FirstOrDefault();
                    _db.Quizzes.Remove(quizEntity);
                    break;

                case nameof(Question):
                    var questionEntity = _db.Questions.Where(c => c.QuestionId == id).FirstOrDefault();
                    _db.Questions.Remove(questionEntity);
                    break;

                case nameof(Option):
                    var optionEntity = _db.Options.Where(c => c.OptionId == id).FirstOrDefault();
                    _db.Options.Remove(optionEntity);
                    break;
            }
        }

        public T Edit(string controllerName, T entity, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    var newQuiz = entity as Quiz;
                    var existingQuiz = _db.Quizzes.Where(c => c.QuizId == id).FirstOrDefault();

                    // mapping
                    existingQuiz.QuizTitle = newQuiz.QuizTitle;
                    existingQuiz.QuizDate = newQuiz.QuizDate;
                    existingQuiz.QuizPersonName = newQuiz.QuizPersonName;
                    existingQuiz.QuizPassNumber = newQuiz.QuizPassNumber;

                    return existingQuiz as T;

                case nameof(Question):
                    var newQuestion = entity as Question;
                    var existingQuestion = _db.Questions.Where(c => c.QuestionId == id).FirstOrDefault();

                    // mapping
                    existingQuestion.QuestionTopic = newQuestion.QuestionTopic;
                    existingQuestion.QuestionText = newQuestion.QuestionText;
                    existingQuestion.QuestionImage = newQuestion.QuestionImage;
                    existingQuestion.QuizId = newQuestion.QuizId;

                    return existingQuestion as T;

                case nameof(Option):
                    var newOption = entity as Option;
                    var existingOption = _db.Options.Where(c => c.OptionId == id).FirstOrDefault();

                    // mapping
                    existingOption.OptionText = newOption.OptionText;
                    existingOption.OptionNumber = newOption.OptionNumber;
                    existingOption.QuestionId = newOption.QuestionId;

                    return existingOption as T;

                default:
                    return null;
            }
        }

        public List<T> GetAll(string controllerName)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    return _db.Quizzes as List<T>;

                case nameof(Question):
                    return _db.Questions as List<T>;

                case nameof(Option):
                    return _db.Options as List<T>;

                default:
                    return null;
            }
        }

        public List<T> GetAllForEndpoint(string endpoint)
        {
            if (endpoint.Contains("Option"))
            {
                int optionId = int.Parse(endpoint.Split('/').LastOrDefault());
                return _db.Options.Where(c => c.OptionId == optionId).ToList() as List<T>;
            }
            return null;
        }

        public T GetSingle(string controllerName, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    return _db.Quizzes.Where(c => c.QuizId == id).FirstOrDefault() as T;

                case nameof(Option):
                    return _db.Options.Where(c => c.OptionId == id).FirstOrDefault() as T;

                default:
                    return null;
            }
        }

        public List<T> GetAllForParentId(string controllerName, string endpointName, int id)
        {
            throw new NotImplementedException();
        }

        T IApiRequest<T>.GetSingleForEndpoint(string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}