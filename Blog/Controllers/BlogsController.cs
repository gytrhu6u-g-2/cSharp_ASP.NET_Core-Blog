using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;

namespace Blog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogContext _context;

        public BlogsController(BlogContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            if (blogs == null)
            {
                Problem("Entity set 'BlogContext.Blogs'  is null.");
            }

            return View(blogs);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blogs = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogs == null)
            {
                return NotFound();
            }

            return View(blogs);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,Title,Created_date,Updated_date")] Blogs blogs)
        {
            //本日の日付を取得
            DateTime dt = DateTime.Now.Date;


            if (!ModelState.IsValid)
            {
                return View(blogs);
            }

            try
            {
                _context.Add(new Blogs
                {
                    Name = blogs.Name,
                    Content = blogs.Content,
                    Title = blogs.Title,
                    Created_date = dt,
                    Updated_date = dt
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "問題が発生しました。");
            }

            return RedirectToAction("Index");
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blogs = await _context.Blogs.FindAsync(id);
            if (blogs == null)
            {
                return NotFound();
            }
            return View(blogs);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Title,Created_date,Updated_date")] Blogs blogs)
        {

            //本日の日付を取得
            DateTime dt = DateTime.Now.Date;

            if (id != blogs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var blog = _context.Blogs.Single(x => x.Id == blogs.Id);
                try
                {
                    blog.Name = blogs.Name;
                    blog.Title = blogs.Title;
                    blog.Content = blogs.Content;
                    blog.Updated_date = dt;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogsExists(blogs.Id))
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
            return View(blogs);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> ToDeletePage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }

            return View("Delete", blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'BlogContext.Blogs'  is null.");
            }
            var blogs = await _context.Blogs.FindAsync(id);
            if (blogs != null)
            {
                _context.Blogs.Remove(blogs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogsExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}