using Microsoft.AspNetCore.Mvc;
using WebApplication_Week2.Models;
namespace WebApplication_Week2.Controllers
{
    public class ProjectsConstroller : Controller
    {
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name = "Project 1 ", Description = "Details of Project 1"}
            };

            return View(projects);
        }

        private static List<Project> _projects = new List<Project>()
        {
            new Project{ProjectId = 1, Name = "Project 2", Description = "First Paginated Project"}
        };

        [HttpGet]

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create ()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(Project project)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Details of Project" + id };
            return View(project);
        }

    }
}
