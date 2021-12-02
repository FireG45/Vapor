using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vapor.Areas.Identity.Data;
using Vapor.Data;
using Vapor.Models;

namespace Vapor.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly StoreContext _context;
        private readonly UsersContext _ucontext;
        private readonly UserManager<User> userManager;
        
        public ReviewsController(StoreContext context, UsersContext ucontext, UserManager<User> userManager)
        {
            _context = context;
            _ucontext = ucontext;
            this.userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reviews.Include(r => r.Item).ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

     
        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["Items"] = _context.Items.ToList();
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Text,Score,Item")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Id = Guid.NewGuid();
                //review.Author = "asdfasdfsadf";//868//userManager.GetUserId(User);
                review.Item = _context.Items.FirstOrDefault(item => item.Id == review.Item.Id);
                review.Date = DateTime.Now;
                //_context.Items.FirstOrDefault(item => item.Id == review.Item.Id).Reviews.Add(review);
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Text,Score")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.Date = DateTime.Now;
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
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
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var review = await _context.Reviews
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            


            if (review == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));

            //return View(review);
        }

        // POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var review = await _context.Reviews.FindAsync(id);
        //    _context.Reviews.Remove(review);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ReviewExists(Guid id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
