using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Question
    {
        public int questionId{get; set;}
        public string questionToipc { get; set; }
        public string questionText { get; set; }
        public string questionImage{ get; set; }
        public int quizId { get; set; }
        public string options { get; set; }
    }
}
