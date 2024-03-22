using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;

namespace blog.Controllers
{
    public class UserBlogsController : Controller
    {
        private readonly BlogContext _context;

        public UserBlogsController(BlogContext context)
        {
            _context = context;
        }

        // GET: UserBlogs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var blogContext = _context.UserBlogs.Include(u => u.User);
            return View(await blogContext.ToListAsync());
        }

        // GET: UserBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var userBlog = await _context.UserBlogs
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBlog == null)
            {
                return NotFound();
            }

            return View(userBlog);
        }
        // GET: UserBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var userBlog = await _context.UserBlogs
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBlog == null)
            {
                return NotFound();
            }

            return View(userBlog);
        }

        // POST: UserBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var userBlog = await _context.UserBlogs.FindAsync(id);
            if (userBlog != null)
            {
                _context.UserBlogs.Remove(userBlog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBlogExists(int id)
        {
            return _context.UserBlogs.Any(e => e.Id == id);
        }
    }
}
