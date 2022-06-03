using PerfectPolicyQuizTwo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Option
    {
        [Display(Name = "Option Id")]
        public int OptionId { get; set; }
        [Display(Name = "Option Text")]
        public string OptionText { get; set; }
        [Display(Name = "Option Number")]
        public string OptionNumber { get; set; }
        [Display(Name = "Question Id")]
        public int QuestionId { get; set; }
    }
}
