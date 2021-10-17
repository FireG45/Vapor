using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vapor.Data;
using Vapor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Vapor.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Vapor.Controllers
{
    public class ItemsController : Controller
    {
        private readonly StoreContext _context;
        private readonly UsersContext _ucontext;
        private readonly SignInManager<User> signInManager;
        User user;

        public ItemsController(StoreContext context, UsersContext ucontext, SignInManager<User> signInManager)
        {
            _context = context;
            _ucontext = ucontext;
            this.signInManager = signInManager;
        }

        // GET: Items

        public async Task<IActionResult> Index(int page = 1, string filterBy = "", [FromQuery(Name = "sort-by")] string sortBy = "")
        {

            IQueryable<Item> sortedItems = sortBy switch
            {
                "name" => _context.Items.OrderBy(item => item.Name),
                "description" => _context.Items.OrderBy(item => item.Description),
                "price" => _context.Items.OrderBy(item => item.Price),
                "nameD" => _context.Items.OrderByDescending(item => item.Name),
                "descriptionD" => _context.Items.OrderByDescending(item => item.Description),
                "priceD" => _context.Items.OrderByDescending(item => item.Price),
                _ => _context.Items
            };

            int pageSize;
            if (filterBy == "")
                pageSize = 10;
            else
                pageSize = _context.Items.Count();

            int pageCount = (int)Math.Ceiling(_context.Items.Count() / (double)pageSize);




            ViewData["Tags"] = _context.Tags.ToList();
            ViewData["FilterBy"] = filterBy;

            ViewData["PageCount"] = pageCount;
            ViewData["CurrentPage"] = page;
            ViewData["SortBy"] = sortBy;

            user = await signInManager.UserManager.GetUserAsync(User);
            List<Item> shoplist;
            if (user?.ShopCart != null)
                shoplist = JsonSerializer.Deserialize<List<Item>>(user.ShopCart);
            else
                shoplist = new();

            ViewBag.SCart = shoplist;

            List<Item> wlist;
            if (user?.WishList != null)
                wlist = JsonSerializer.Deserialize<List<Item>>(user.WishList);
            else
                wlist = new();

            ViewBag.Wlist = wlist;

            return View(await sortedItems
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync());
        }

        public async Task<IActionResult> Filter(string F)
        {
            ViewData["Tags"] = _context.Tags.ToList();
            ViewData["F"] = F;
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            ViewData["Reviews"] = _context.Reviews.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["Tags"] = _context.Tags.ToList();
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Img,Tag1,Tag2,Tag3")] Item item)
        {

            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["Tags"] = _context.Tags.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Img,Tag1,Tag2,Tag3")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(Guid id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    Text = collection["r.Text"],
                    Score = Convert.ToInt32(collection["r.Score"]),
                    Date = DateTime.Now
                };
                review.Id = Guid.NewGuid();
                review.Item = _context.Items.FirstOrDefault(item => item.Id == id);
                _context.Add(review);

                await _context.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Items", action = "Details", id = id });
            }
            return BadRequest();
        }

        [Authorize]
        public async Task<IActionResult> ToCart(Guid Id)
        {
            user = await signInManager.UserManager.GetUserAsync(User);
            var item = _context.Items.FirstOrDefaultAsync(m => m.Id == Id).Result;
            List<Item> shoplist;
            if (user.ShopCart != null)
                shoplist = JsonSerializer.Deserialize<List<Item>>(user.ShopCart);
            else
                shoplist = new();
            shoplist.Add(item);
            user.ShopCart = JsonSerializer.Serialize<List<Item>>(shoplist);
            _ucontext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteFromCart(Guid Id)
        {
            user = await signInManager.UserManager.GetUserAsync(User);
            var item = _context.Items.FirstOrDefaultAsync(m => m.Id == Id).Result;
            List<Item> shoplist;
            if (user.ShopCart != null)
                shoplist = JsonSerializer.Deserialize<List<Item>>(user.ShopCart);
            else
                shoplist = new();
            shoplist.Remove(shoplist.Find(m => m.Id==Id));
            user.ShopCart = JsonSerializer.Serialize<List<Item>>(shoplist);
            _ucontext.SaveChanges();
            return RedirectToAction(nameof(ShopCart));
        }
        [Authorize]
        public async Task<IActionResult> ToWlist(Guid Id)
        {
            user = await signInManager.UserManager.GetUserAsync(User);
            var item = _context.Items.FirstOrDefaultAsync(m => m.Id == Id).Result;
            List<Item> wlist;
            if (user.WishList != null)
                wlist = JsonSerializer.Deserialize<List<Item>>(user.WishList);
            else
                wlist = new();
            wlist.Add(item);
            user.WishList = JsonSerializer.Serialize<List<Item>>(wlist);
            _ucontext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ShopCart()
        {
            user = await signInManager.UserManager.GetUserAsync(User);
            List<Item> items;

            if (user.ShopCart != null)
                items = JsonSerializer.Deserialize<List<Item>>(user.ShopCart);
            else
                items = new();

            return View(items);
        }
    }
}
