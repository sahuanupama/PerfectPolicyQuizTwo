using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Quiz
    {
        [Display(Name = "Quiz Id")]
        public int QuizId { get; set; }
        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }
        [Display(Name = "Quiz Date")]
        public DateTime QuizDate { get; set; }
        [Display(Name = "Quiz Person Name")]
        public string QuizPersonName { get; set; }
        [Display(Name = "Quiz Pass Number")]
        public int QuizPassNumber { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
