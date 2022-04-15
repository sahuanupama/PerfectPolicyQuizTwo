using PerfectPolicyQuizTwo.Models.QuestionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Option
    {
        public int OptionId { get; set; }

        public string OptionText { get; set; }

        public string OptionNumber { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
