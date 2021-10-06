using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vapor.Data;
using Vapor.Models;

namespace Vapor.Controllers
{
    public class TagsController : Controller
    {
        private readonly StoreContext _context;

        public TagsController(StoreContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.ToListAsync());
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tag")] Tag tg)
        {
            if (ModelState.IsValid)
            {
                tg.Id = Guid.NewGuid();
                _context.Add(tg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return View(tg);
            return RedirectToAction(nameof(Create));
        }





        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tg = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tg == null)
            {
                return NotFound();
            }

            return View(tg);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tg = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
