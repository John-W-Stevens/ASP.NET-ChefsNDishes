using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // for session
using Microsoft.AspNetCore.Identity; // for password hashing
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {

        private CNDContext dbContext;

        public HomeController(CNDContext context)
        {
            dbContext = context;
        }

        // Base route
        [HttpGet("")]
        public IActionResult Index()
        {
            Chef[] AllChefs = dbContext.Chefs.ToArray();
            return View("Index", AllChefs);
        }

        ///////////// BEGINNING OF CRUD METHODS FOR CHEF MODEL /////////////

        // GET ALL Chefs
        [HttpGet("chefs")]
        public IActionResult Chefs()
        {
            Chef[] AllChefs = dbContext.Chefs.ToArray();
            return View("Chefs", AllChefs);
        }

        //  GET One Single Chef (Read/Update/Delete this chef)
        [HttpGet("chef/{chefId}")]
        public IActionResult Chef(int chefId)
        {
            Chef OneSingleChef = dbContext.Chefs.FirstOrDefault(b => b.ChefId == chefId);
            return View("EditChefForm", OneSingleChef);
        }

        // POST Update One Single Chef
        [HttpPost("chef/{chefId}")]
        public IActionResult UpdateChef(int chefId, Chef chef)
        {
            if (ModelState.IsValid)
            {
                chef.ChefId = chefId;
                dbContext.Update(chef);
                dbContext.Entry(chef).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();
                return RedirectToAction("Chefs");
            }
            else
            {
                Chef OneSingleChef = dbContext.Chefs.FirstOrDefault(b => b.ChefId == chefId);
                return View("EditChefForm", OneSingleChef);
            }
        }

        // POST Delete One Single Chef
        [HttpPost("chef/{chefId}/delete")]
        public IActionResult DeleteChef(int chefId)
        {
            Chef OneSingleChef = dbContext.Chefs.FirstOrDefault(b => b.ChefId == chefId);
            dbContext.Chefs.Remove(OneSingleChef);
            dbContext.SaveChanges();
            return RedirectToAction("Chefs");
        }

        // GET Chef/create Page
        [HttpGet("chef/create")]
        public IActionResult CreateChefForm()
        {
            return View();
        }

        // POST Create One Single Chef
        [HttpPost("chef/create")]
        public IActionResult CreateChef(Chef chef)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(chef);
                dbContext.SaveChanges();
                return RedirectToAction("Chefs");
            }
            return View("CreateChefForm");
        }

        ///////////// END OF CRUD METHODS FOR CHEF MODEL /////////////

        ///////////// BEGINNING OF CRUD METHODS FOR DISH MODEL /////////////

        // GET ALL Dishes
        [HttpGet("dishes")]
        public IActionResult Dishes()
        {

            Dish[] AllDishes = dbContext.Dishes
                .Include(d => d.Creator)
                .ToArray();

            //Dish[] AllDishes = dbContext.Dishes.ToArray();
            return View("Dishes", AllDishes);
        }

        //  GET One Single Dish (Read/Update/Delete this dish)
        [HttpGet("dish/{dishId}")]
        public IActionResult Dish(int dishId)
        {
            Dish OneSingleDish = dbContext.Dishes.FirstOrDefault(b => b.DishId == dishId);
            return View("EditDishForm", OneSingleDish);
        }

        // POST Update One Single Dish
        [HttpPost("dish/{dishId}")]
        public IActionResult UpdateDish(int dishId, Dish dish)
        {
            if (ModelState.IsValid)
            {
                dish.DishId = dishId;
                dbContext.Update(dish);
                dbContext.Entry(dish).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                Dish OneSingleDish = dbContext.Dishes.FirstOrDefault(b => b.DishId == dishId);
                return View("EditDishForm", OneSingleDish);
            }
        }

        // POST Delete One Single Dish
        [HttpPost("dish/{dishId}/delete")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish OneSingleDish = dbContext.Dishes.FirstOrDefault(b => b.DishId == dishId);
            dbContext.Dishes.Remove(OneSingleDish);
            dbContext.SaveChanges();
            return RedirectToAction("Dishes");
        }

        // GET Dish/create Page
        [HttpGet("dish/create")]
        public IActionResult CreateDishForm()
        {
            //Chef[] AllChefs = dbContext.Chefs.ToArray();
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.Chefs = AllChefs;
            return View();
        }

        // POST Create One Single Dish
        [HttpPost("dish/create")]
        public IActionResult CreateDish(Dish dish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.Chefs = AllChefs;
            return View("CreateDishForm");
        }

        ///////////// END OF CRUD METHODS FOR DISH MODEL /////////////

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

