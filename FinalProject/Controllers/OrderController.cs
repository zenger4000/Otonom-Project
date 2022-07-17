using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ShoppingCart shoppingCart;
        private readonly ApplicationDbContext dbContext;
        public OrderController(ApplicationDbContext _dbContext, IOrderRepository _orderRepository, ShoppingCart _shoppingCart)
        {
            orderRepository = _orderRepository;
            shoppingCart = _shoppingCart;
            dbContext = _dbContext;
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            shoppingCart.ShoppingCartItems = shoppingCart.GetShoppingCartItems();
            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your Cart Is Empty !!!");
            }
            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckOutComplete");
            }
            return View(order);
        }

        public IActionResult CheckOutComplete()
        {
            ViewBag.CheckOutCompleteMessage = "Thank You For Your Order ..!";
            return View();
        }

        public IActionResult OrderList()
        {
            var ordersList = dbContext.orders.OrderByDescending(o => o.OrderId);
            return View(ordersList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = dbContext.orders.Find(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            dbContext.Update(order);
            dbContext.SaveChanges();
            return RedirectToAction("OrderList");
        }
        public IActionResult OrderDetails(int id)
        {
            var order = dbContext.orderDetails.Select(e => e).Where(e => e.OrderId == id).FirstOrDefault();
            return View(order);
        }
    }
}
