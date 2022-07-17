using FinalProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.ViewModels;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly Configuration
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles ="admin")]
        // GET: Products
        public IActionResult Index()
        {
            var items = _context.Products.ToList();
            return View(items);
        }


        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.Include(x => x.Reviews).FirstOrDefaultAsync(m => m.id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        
        // GET: Products/Create
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            return View();

        }
        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Discription,Picture,Details,price")] productviewmodel vm)
        {

            string stringFileName = UploadFile(vm);
            var products = new Products { Name = vm.Name, Picture = stringFileName, Discription = vm.Discription,Details=vm.Details, price = vm.price };


            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           

            return View(products);
        }

        private string UploadFile(productviewmodel vm)
        {
            string fileName = null;
            if (vm.Picture != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "ProductImages");
                fileName = Guid.NewGuid().ToString() + "-" + vm.Picture.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Picture.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Discription,Picture,Details,price")] productviewmodel  vm)
        {
            //if (id != products.id)
            //{
            //    return NotFound();
            //}

            string stringFileName = UploadFile(vm);
            var products = new Products {id=id, Name = vm.Name, Picture = stringFileName, Discription = vm.Discription, Details = vm.Details, price = vm.price };



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProductsExists(products.id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

        public IActionResult Review(int productId) 
        {
                var email = _context.Reviews.Select(e => new { e.ClientEmail, e.ProductID }).Where(e => e.ProductID == productId && e.ClientEmail == User.Identity.Name).FirstOrDefault();

            try
            {
                if (User.Identity.Name != email.ClientEmail)
                {
                    var r = new Review() { ProductID = productId };
                    return View(r);
                }
                else
                {
                    var review = _context.Reviews.Select(e => e).Where(e => e.ProductID == productId);
                    return View(review);
                }
            }
            catch (Exception)
            {

                var r = new Review() { ProductID = productId };
                return View(r);
            }
            
            
            
        }
        [HttpPost]
        public IActionResult Review(Review review)
        {
            _context.Reviews.Add(review);
            //try
            //{
                _context.SaveChanges();
                return RedirectToAction("index", "home");
            //}
            //catch (Exception)
            //{
            //    return View("reviewfound");
                
            //}

            //return View(review);
        }

        public IActionResult reviewfound()
        {
            return View();
        }
    }
}
