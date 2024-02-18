using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Week04.Data;
using Week04.Models;
using Week04.Models.Entities;

namespace Week04.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbcontext;

        public CoursesController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
    
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddCoursesViewModel viewModel)
        {
            var Courses = new Courses() 
            {
                Name = viewModel.Name,
                Code = viewModel.Code,
                Semester = viewModel.Semester,
                Subscribe = viewModel.Subscribe

            };
            await dbcontext.Courses.AddAsync(Courses);
            await dbcontext.SaveChangesAsync();

            return View();

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbcontext.Courses.ToListAsync();

            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var Courses = await dbcontext.Courses.FindAsync(Id);
            return View(Courses);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(Courses viewModel)
        {
            var Courses = await dbcontext.Courses.FindAsync(viewModel.Id);
            if (Courses is not null)
            {
                Courses.Name = viewModel.Name;
                Courses.Code = viewModel.Code;
                Courses.Semester = viewModel.Semester;
                Courses.Subscribe  = viewModel.Subscribe;
                await dbcontext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Courses");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Courses viewModel)
        {
            var Courses = await dbcontext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (Courses is not null)
            {
                dbcontext.Courses.Remove(viewModel);
                await dbcontext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Courses");
        }
    }
}
