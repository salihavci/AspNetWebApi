using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; } //Product tablosuna erişmek için dependency injection yaptık
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetWithCategoryByIdAsync(int id)
        {
            var product = await appDbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x=> x.Id == id);
            return product;
        }

    }
}
