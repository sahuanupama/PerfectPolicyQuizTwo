﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuizTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo.Controllers
{
    public class AuthController : Controller
    {
        HttpClient _client;

        public AuthController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Store token and render index page.
        /// </summary>
        /// <param name="user"> User credentials for the login</param>
        /// <returns>Index view</returns>
        [HttpPost]
        public IActionResult Login(UserInfo user)
        {
            string token = "";

            var response = _client.PostAsJsonAsync("Auth/GenerateToken", user).Result;

            if (response.IsSuccessStatusCode)
            {
                // logged in
                token = response.Content.ReadAsStringAsync().Result.Trim('"');

                // Store the token in the session
                HttpContext.Session.SetString("Token", token);
            }
            else
            {
                // there was an issue logging in
                ViewBag.Error = "The provided credentials were incorrect";
                // potentially save a message to ViewBag and render in the view
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Session finish
        /// </summary>
        /// <returns>Index page</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
