using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Models
{
    public class Option
    {
        public int optionId { get; set; }
        public int optionText { get; set; }
        public int optionNumber { get; set; }
        public int questionId { get; set; }
        public int question { get; set; }
    }
}
