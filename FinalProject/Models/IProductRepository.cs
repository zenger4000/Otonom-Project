using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
   public interface IProductRepository
    {
        IEnumerable<Products> GetAllProducts { get; }
        Products GetProductById(int productId);
    }
}
