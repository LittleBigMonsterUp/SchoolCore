using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCore.EntityFramework;
using SchoolCore.ViewModels;

namespace SchoolCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly SchoolDbContext _context;

        public HomeController(SchoolDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "学生统计信息";



            var entities = from students in _context.Students
                           group students by students.EnrollmentDate
                into dateGroup
                           select new EnrollmentDateGroup()
                           {
                               EnrollmenDate = dateGroup.Key,
                               StudentCount = dateGroup.Count()
                           };


            var dtos = await entities.AsNoTracking().ToListAsync();

            return View(dtos);
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
