using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Helper
{
    public class AuthenticationHelper
    {
        public static bool isAuthenticated(HttpContext context)
        {
            return context.Session.Keys.Any(c => c.Equals("Token"));
        }
    }
}
