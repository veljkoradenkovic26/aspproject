using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.User;
using Application.SearchObjects;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IGetUserFromLoginForm _getUserCommand;

        public HomeController(IGetUserFromLoginForm getUserCommand)
        {
            _getUserCommand = getUserCommand;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UserSearch model)
        {
            try
            {
                var find = _getUserCommand.Execute(model);
                HttpContext.Session.SetObjectAsJson(find);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Invalid credentials!";
                return View();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
