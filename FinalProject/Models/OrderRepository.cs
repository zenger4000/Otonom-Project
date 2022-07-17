using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ShoppingCart shoppingCart;
        public OrderRepository(ApplicationDbContext _dbContext, ShoppingCart _shoppingCart)
        {
            dbContext = _dbContext;
            shoppingCart = _shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCart.GetShoppingCartTotal();
            dbContext.orders.Add(order);
            dbContext.SaveChanges();
            var shoppingCartItems = shoppingCart.GetShoppingCartItems();
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    Price = shoppingCartItem.Product.price,
                    ProductId = shoppingCartItem.Product.id,
                    OrderId = order.OrderId
                };
                dbContext.orderDetails.Add(orderDetail);
            }
            dbContext.SaveChanges();
        }
    }
}
