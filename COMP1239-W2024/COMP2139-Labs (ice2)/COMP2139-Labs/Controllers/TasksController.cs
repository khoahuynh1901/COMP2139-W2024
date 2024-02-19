using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using COMP2139_Labs.Models;
using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Net.WebSockets;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace COMP2139_Labs.Controllers
{
    [Route("Tasks")]
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext context) 
        {
            _db = context;
        }

        [HttpGet("Index/{projectId:int}")]
        public IActionResult Index(int projectId)
        {
            var taskname = "mytask";
            
            var tasks = _db.ProjectTasks.
                Where(t => t.ProjectId == projectId).
                ToList();
            /*
            var result = _db.ProjectTasks.Where(current => current.Title == taskname)
                .ToList();
            */

            

            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            var task = _db.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefault(t => t.ProjectTaskId == id);

            if (task == null)
            { 
                return NotFound();
            }
            return View(task);
        }
        /*
        [HttpGet]
        public IActionResult Details(int id) 
        { 
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        */

        [HttpGet("Create{projectId:int}")]
        public IActionResult Create(int projectId)
        {
            var project = _db.Projects.Find(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var task = new ProjectTask
            {
                ProjectId = projectId
            };

            return View(task);
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _db.ProjectTasks.Add(task);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index), new {projectId = task.ProjectId});
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (id != task.ProjectTaskId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(task);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);

            //1:33:19
        }

        [HttpGet("Delete/{int:int}")]
        public IActionResult Delete(int id) 
        {
            var project = _db.Projects.FirstOrDefault(t => t.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            
            return View(project);
        }

        [HttpGet]
        public IActionResult DeleteConfirmed(int ProjectTaskId)
        {
            var task = _db.ProjectTasks.Find(ProjectTaskId);
            if (task != null)
            {
                _db.ProjectTasks.Remove(task);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            
            return NotFound();
        }


        [HttpGet("Search/{projectId:int}/{searchString?}")]
        public async Task<IActionResult> Search(int projectId, string searchString)
        {
            var tasksQuery = _db.ProjectTasks.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            { 
                tasksQuery = tasksQuery.Where(t => t.Title.Contains(searchString) || t.Description.Contains(searchString));
            }

            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectId = projectId;
            return View("Index", tasks);
        }
    }
}
