using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaUI.Data;
using System.Security.Claims;

namespace MediaUI
{
    public class SportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sport category posts
        public async Task<IActionResult> Index()
        {
            var posts = _context.Post.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            if (userId != null)
            {
                posts = posts.Where(p => p.CreatedBy == new Guid(userId)).ToList();
            }
            else
            {
                posts.Clear();
            }

            return View(posts);
            
        }

        // GET: MyPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: MyPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Report,CreatedDate,CreatedBy,Category=?Sport")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: MyPosts/Details/5
        public async Task<IActionResult> Category(int? id=1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }



        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
