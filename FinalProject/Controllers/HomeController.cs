using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public List<Products> Products;
        private readonly ApplicationDbContext context;
        
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
            Products = new List<Products> ();

            
        }
        [AllowAnonymous]
        // GET: Products
        public ActionResult Index() 
        {
            var model = new MyViewModel();
            model.Products = context.Products.ToList();
            model.Teams = context.Teams.ToList();
            return View(model);
            
        }

        //[HttpGet]
        //public IActionResult ContactUs()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult SaveContact(ContactUs model)
        //{
        //    context.ContactUs.Add(model);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
       

    }
}
