using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {

        // dykstra3
        // class variable for the database context
        private readonly SchoolContext _context;
        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Name"] = "Thuan Ho";
            ViewData["Email"] = "hot4@uw.edu";
            ViewData["Github"] = "https://github.com/thuanho124";
            ViewData["Message"] = "If you have any questions, feel free to contact me";

            return View();
        }

        // dykstra3
        // controller for the about page
        // the code first select student count and year rank, instead of enrollment date in the tutorial
        // then use IQueyable to group student's count by year rank 
        public async Task<ActionResult> About()
        {
            // group student's count by year rank
            // and then store the list into a collection of EnrollementDateGroup view model
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.YearRank into dateGroup

                // select count of the student and year rank 
                select new EnrollmentDateGroup()
                {
                    YearRank = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };

            
            // configure the view for the about page 
            return View(await data.AsNoTracking().ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
