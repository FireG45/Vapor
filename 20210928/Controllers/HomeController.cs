using Vapor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vapor.Data;
using Microsoft.AspNetCore.Identity;
using Vapor.Areas.Identity.Data;
using System.Text.Json;

namespace Vapor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreContext _context;
        private readonly UsersContext _ucontext;
        private readonly SignInManager<User> signInManager;
        User user;

        public HomeController(ILogger<HomeController> logger, StoreContext context, UsersContext ucontext, SignInManager<User> signInManager)
        {
            _logger = logger;
            _context = context;
            _ucontext = ucontext;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            Random r = new Random();

            var arr = _context.Items.ToArray<Item>();

            var user = await signInManager.UserManager.GetUserAsync(User);
            List<Item> items;

            if (user.WishList != null)
                items = JsonSerializer.Deserialize<List<Item>>(user.WishList);
            else
                items = new();

            if (items.Count >= 3)
            {
                var c1 = items[r.Next(0, items.Count)];
                var c2 = items[r.Next(0, items.Count)];
                var c3 = items[r.Next(0, items.Count)];

                while ((c1 == c2) && (c2 == c3) && (c1 == c3))
                {
                    c1 = items[r.Next(0, items.Count)];
                    c2 = items[r.Next(0, items.Count)];
                    c3 = items[r.Next(0, items.Count)];
                }

                ViewData["IsWlist"] = true;
                ViewData["CItem1"] = c1;
                ViewData["CItem2"] = c2;
                ViewData["CItem3"] = c3;
            }
            else
            {
                var c1 = arr[r.Next(0, arr.Length)];
                var c2 = arr[r.Next(0, arr.Length)];
                var c3 = arr[r.Next(0, arr.Length)];

                while ((c1 == c2) && (c2 == c3) && (c1 == c3))
                {
                    c1 = arr[r.Next(0, arr.Length)];
                    c2 = arr[r.Next(0, arr.Length)];
                    c3 = arr[r.Next(0, arr.Length)];
                }

                ViewData["IsWlist"] = false;
                ViewData["CItem1"] = c1;
                ViewData["CItem2"] = c2;
                ViewData["CItem3"] = c3;
            }

            var TagArr = _context.Tags.ToList<Tag>();
            var tag = TagArr[r.Next(0, TagArr.Count)].tag;
            //var Arr = .ToList<Item>();

            List<Item> TagItems = new();
            while(TagItems.Count<3)
            {
                TagItems.Clear();
                tag = TagArr[r.Next(0, TagArr.Count)].tag;
                foreach (Item i in _context.Items)
                {
                    if (i.Tag1 == tag || i.Tag2 == tag || i.Tag3 == tag)
                        TagItems.Add(i);
                }
            }
            ViewData["Tag"] = tag;
            ViewData["TagItems"] = TagItems;



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
