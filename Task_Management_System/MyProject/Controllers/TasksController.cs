using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Security.Claims;
using Task = MyProject.Models.Task;

namespace MyProject.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskMsContext _context;

        public TasksController(TaskMsContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string searchString)
        {
            // Retrieve user ID from the currently logged-in user
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert userId to integer
            if (int.TryParse(userId, out int intUserId))
            {
                // Fetch tasks associated with the current user
                var tasks = from t in _context.Tasks
                            where t.UserId == intUserId
                            select t;

                // Apply search filter
                if (!string.IsNullOrEmpty(searchString))
                {
                    tasks = tasks.Where(t => t.TaskName.Contains(searchString)
                                           || t.TaskType.Contains(searchString)
                                           || t.TaskDescription.Contains(searchString));
                }

                ViewData["CurrentFilter"] = searchString;

                return View(await tasks.ToListAsync());
            }

            return View(new List<Task>());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert userId to integer
            if (task == null || !int.TryParse(userId, out int intUserId) || task.UserId != intUserId)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,TaskType,TaskDescription,StartDate,EndDate,Duration")] Task task)
        {
            if (ModelState.IsValid)
            {
                // Retrieve user ID from the currently logged-in user
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Convert userId to integer
                if (int.TryParse(userId, out int intUserId))
                {
                    // Associate the task with the current user
                    task.UserId = intUserId;

                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert userId to integer
            if (task == null || !int.TryParse(userId, out int intUserId) || task.UserId != intUserId)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,TaskType,TaskDescription,StartDate,EndDate,Duration")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing task from the database
                    var existingTask = await _context.Tasks.FindAsync(id);
                    if (existingTask == null)
                    {
                        return NotFound();
                    }

                    // Ensure the task belongs to the current user
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (!int.TryParse(userId, out int intUserId) || existingTask.UserId != intUserId)
                    {
                        return Forbid();
                    }

                    // Update the task properties
                    existingTask.TaskName = task.TaskName;
                    existingTask.TaskType = task.TaskType;
                    existingTask.TaskDescription = task.TaskDescription;
                    existingTask.StartDate = task.StartDate;
                    existingTask.EndDate = task.EndDate;
                    existingTask.Duration = task.Duration;

                    // Attach the existing task and mark it as modified
                    _context.Entry(existingTask).State = EntityState.Modified;

                    // Save changes
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

   

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert userId to integer
            if (task == null || !int.TryParse(userId, out int intUserId) || task.UserId != intUserId)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert userId to integer
            if (task != null && int.TryParse(userId, out int intUserId) && task.UserId == intUserId)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
