using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneByGeneAssessment.Models;

namespace GeneByGeneAssessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly GBGContext _context;

        public HomeController(GBGContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
