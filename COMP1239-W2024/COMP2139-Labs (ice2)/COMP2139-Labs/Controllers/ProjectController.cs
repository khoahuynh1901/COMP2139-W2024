using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Models;
using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace COMP2139_Labs.Controllers
{
    public class ProjectController: Controller
    {
        private readonly AppDbContext _db;
        public ProjectController(AppDbContext db) 
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            /*
            var projects = new List<Project>()
            {
                new Project() { ProjectId = 1, Name = "Project 1", Description = "First Project" },
                new Project() { ProjectId = 2, Name = "Project 2", Description = "Second Project" }
            };
            */
            return View(_db.Projects.ToList()); 
        }
        [HttpGet]
        public IActionResult About() { return View(); }
        
        
        [HttpGet]        
        public IActionResult Create() { return View(); }

        [HttpGet]
        public IActionResult Delete() 
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project) 
        {
            if (ModelState.IsValid) 
            {
                _db.Projects.Add(project);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = _db.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(project);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();

                    }
                    else 
                    {
                        throw;
                    }
                }
            }


            return View(project);
        }

        private bool ProjectExists(int id)
        {
            return _db.Projects.Any(e => e.ProjectId == id);
        }

        public IActionResult Delete(int id)
        { 
            var project = _db.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        public async Task<IActionResult> Search(string searchString)
        { 
            var projectsQuery = from p in _db.Projects select p;

            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            if (searchPerformed)
            {
                projectsQuery = projectsQuery.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString));
            }

            var projects = await projectsQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", projects); //reuse the index value to display results
        }
    }
}
