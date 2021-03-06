using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models.QuizModel
{
    public class QuizCreate
    {
        [StringLength(10, ErrorMessage = "Name exceeds 100 character")]
        public int quizId { get; set; }
        public string quizTitle { get; set; }
        public DateTime quizdate { get; set; }
        public string quizpresonName { get; set; }
        public string quizPassNumber { get; set; }
        public string questionNumber { get; set; }

    }
}
