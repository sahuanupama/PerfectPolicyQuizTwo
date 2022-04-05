using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models.QuestionModel
{
    public class QuestionCreate
    {
        public int questionId { get; set; }
        public string questionToipc { get; set; }
        public int quizId { get; set; }
        public string options { get; set; }

    }
}
