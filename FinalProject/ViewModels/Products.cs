using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class Products
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public string Discription { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
