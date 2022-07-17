using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Products products { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Order order { get; set; }
    }
}
