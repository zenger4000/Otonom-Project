using FinalProject.Data;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ShoppingCart
    {
        private ApplicationDbContext _AppContext { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(ApplicationDbContext _AppContext)
        {
            this._AppContext = _AppContext;
        }

        // Adding ShoppingCart
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        //Adding Items to Cart 
        public void AddToCart(Products product, int amount)
        {
            var shoppingCartItem = _AppContext.ShoppingcartItems.SingleOrDefault(
                p => p.Product.id == product.id && p.ShoppingCartId == ShoppingCartId
            );
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = amount
                };
                _AppContext.ShoppingcartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _AppContext.SaveChanges();
        }

        //Remove Item from Cart
        public int RemovefromCart(Products products)
        {
            var shoppingCartItem = _AppContext.ShoppingcartItems.SingleOrDefault(
               p => p.Product.id == products.id && p.ShoppingCartId == ShoppingCartId);
            var LocalAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    LocalAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _AppContext.ShoppingcartItems.Remove(shoppingCartItem);
                }
            }
            _AppContext.SaveChanges();
            return LocalAmount;
        }

        //Getting All items of the Cart 
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems =
                _AppContext.ShoppingcartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product).ToList());
        }

        //clear All items from cart
        public void ClearCart()
        {
            var cartItems = _AppContext.ShoppingcartItems.Where(c => c.ShoppingCartId == ShoppingCartId);
            _AppContext.ShoppingcartItems.RemoveRange(cartItems);
            _AppContext.SaveChanges();
        }

        //Calculating Total Order
        public double GetShoppingCartTotal()
        {
            var total = _AppContext.ShoppingcartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.price * c.Amount).Sum();
            return total;
        }
    }
}
