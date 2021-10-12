using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vapor.Data;
using Vapor.Models;

namespace Vapor.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly StoreContext _context;

        public ShopCartController(StoreContext context)
        {
            _context = context;
        }

        // GET: ShopCartController
        public ActionResult Index(Item item)
        {
            _context.ShopCart.Add(item);
            ViewData["ShopCart"] = _context.ShopCart.ToList<Item>();
            return View();
        }

        // GET: ShopCartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopCartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopCartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
