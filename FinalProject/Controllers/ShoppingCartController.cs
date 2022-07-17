using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController (IProductRepository _productRepository, ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
            productRepository = _productRepository;
        }

        public IActionResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            var shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selected = productRepository.GetAllProducts.FirstOrDefault(p => p.id == productId);
            if (selected != null)
            {
                _shoppingCart.AddToCart(selected, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selected = productRepository.GetAllProducts.FirstOrDefault(p => p.id == productId);
            if (selected != null)
            {
                _shoppingCart.RemovefromCart(selected);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
