using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaUI.Data;
using MediaUI.Models;
using MediaUI.Services;
using System.Security.Claims;

namespace MediaUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PostsApiClient _postsApiClient;

        public PostsController(ApplicationDbContext context, PostsApiClient postsApiClient)
        {
            _context = context;
            _postsApiClient = postsApiClient;
        }

        // GET: posts
        public async Task<IActionResult> Index(string searchString, string myId)
        {
            ViewData["CurrentFilter"] = searchString;

            var posts = _context.Post.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Category.ToString().ToLower().Contains(searchString.ToLower())).ToList();
            }

            if(!String.IsNullOrEmpty(myId))
            {

            }

            return View(posts);

            //This will call the Web API Service
            //return View(await _postsApiClient.GetpostList());

        }
        public ActionResult MyPosts()
        {
           // ViewData["CurrentFilter"] = searchString;
            var posts = _context.Post.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            if (userId != null)
            {
                posts = posts.Where(p => p.CreatedBy == new Guid(userId)).ToList();
            }

          /*  if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Category.ToString().ToLower().Contains(searchString.ToLower())).ToList();
            }*/


            return View("Index",posts);

            //This will call the Web API Service
            //return View(await _postsApiClient.GetpostList());

        }

        // GET: posts/Details/5
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

            var comments =   await _context.Comment.ToListAsync();
            if (comments != null)
            {
                comments = comments.Where(c => c.Report == id).OrderByDescending(c=>c.CreatedDate).ToList();
                //var comments = await _context.Comment.Where(c => c.ReportId.Id == id).ToListAsync();
                post.Comments = comments;
            }
            return View(post);
        }

        // GET: posts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCommentPartial(Comment comment)
        {
           //comment.ReportId = _context.post.
            if (!string.IsNullOrEmpty(comment.Descritption))
            {
                _context.Comment.Add(comment);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Posts", new {  id = comment.Report });

            }
            return PartialView("CreateCommentPartial");
        }
        
        [HttpPost]
        public ActionResult DeleteCommentPartial(Comment comment)
        {
           //comment.ReportId = _context.post.
            if (!string.IsNullOrEmpty(comment.Descritption))
            {
                _context.Comment.Add(comment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("CreateCommentPartial");
        }

        // POST: posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Report,CreatedDate,CreatedBy,Category")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Report,CreatedDate,CreatedBy,Category")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!postExists(post.Id))
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
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
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

        // POST: posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool postExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
