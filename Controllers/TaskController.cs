
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

public class TaskController : Controller
{
    private readonly TaskManagerContext _context;

    public TaskController(TaskManagerContext context)
    {
        _context = context;
    }

    // GET: TASKS
    public async Task<IActionResult> Index(string searchString)    
    {
        if (_context.Task == null)
        {
            return Problem("Entity set 'TaskManagerContext.Task' is nulll");
        }

        var tasks = from m in _context.Task
                    select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            tasks = tasks.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper()));
        }

        return View(await tasks.ToListAsync());
    }

    // GET: TASKS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Task
            .FirstOrDefaultAsync(m => m.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // GET: TASKS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TASKS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] TaskManager.Models.Task task)
    {
        if (ModelState.IsValid)
        {
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    // GET: TASKS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Task.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    // POST: TASKS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description")] TaskManager.Models.Task task)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(task);
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

    // GET: TASKS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Task
            .FirstOrDefaultAsync(m => m.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: TASKS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var task = await _context.Task.FindAsync(id);
        if (task != null)
        {
            _context.Task.Remove(task);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskExists(int? id)
    {
        return _context.Task.Any(e => e.Id == id);
    }
}
