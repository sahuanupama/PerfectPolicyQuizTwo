using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models.Testing
{
    public class TestDatabase
    {
        public List<Quiz> Quizzes { get; set; }
        public List<Question> Questions { get; set; }
        public List<Option> Options { get; set; }
        public List<UserInfo> Users { get; set; }

        public TestDatabase()
        {
            Quizzes = new List<Quiz>
            {
                new Quiz { QuizId = 1, QuizTitle = "Copyright", QuizDate =  DateTime.Now, QuizPersonName = "Anupama", QuizPassNumber=75 },
                new Quiz { QuizId = 2, QuizTitle = "Copyright2", QuizDate =  DateTime.Now, QuizPersonName = "Anupama", QuizPassNumber=78 },
                new Quiz { QuizId = 3, QuizTitle = "Copyright3", QuizDate =  DateTime.Now, QuizPersonName = "Anupama", QuizPassNumber=79 },
                new Quiz { QuizId = 4, QuizTitle = "Copyright4", QuizDate =  DateTime.Now, QuizPersonName = "Anupama", QuizPassNumber=65 }
            };

            Questions = new List<Question>
            {
                new Question{QuestionId=1,QuestionTopic="Fire Safety", QuestionText="What is fire safety?",  QuestionImage="IMG_3811.JPG", QuizId=1 },
                new Question{QuestionId=2,QuestionTopic="Work from Home", QuestionText="Do you like go to office?",  QuestionImage="IMG_3811.JPG", QuizId=1 }
            };


            Options = new List<Option>
            {
            new Option { OptionId=1 , OptionText="My Option 1" , OptionNumber="1" , QuestionId=1 },
            new Option { OptionId=2 , OptionText="My Option 2" , OptionNumber="2" , QuestionId=1 },
            new Option { OptionId=3 , OptionText="My Option 3" , OptionNumber="3" , QuestionId=1 },
            new Option { OptionId=4 , OptionText="My Option 4" , OptionNumber="4" , QuestionId=1 },
            new Option { OptionId=5 , OptionText="My Option 5" , OptionNumber="5" , QuestionId=1 }
            };

            Users = new List<UserInfo>{
                 new UserInfo { Username = "Anupama", Password = "1234_abc" }
            };
        }
    }
}
