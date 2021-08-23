using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Velociraptor.Models;

namespace Velociraptor.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Information = DateTime.Now.ToString();
            return View("Index");
        }
    }
}
