using FinalProject.Data;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext appDbContext;
        public ProductRepository(ApplicationDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public IEnumerable<Products> GetAllProducts
        {
            get
            {
                return appDbContext.Products.ToList();
            }
        }

        public Products GetProductById(int productId)
        {
            return appDbContext.Products.FirstOrDefault(c => c.id == productId);
        }
    }
}
