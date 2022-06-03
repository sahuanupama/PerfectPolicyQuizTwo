using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Question
    {
        [Display(Name = "Question Id")]
        public int QuestionId { get; set; }
        [Display(Name = "Question Topic")]
        public string QuestionTopic { get; set; }
        [Display(Name = "Question Text ")]
        public string QuestionText { get; set; }
        [Display(Name = "Question Image")]
        public string QuestionImage { get; set; }
        [Display(Name = "Quiz Id")]
        public int QuizId { get; set; }
    }
}
