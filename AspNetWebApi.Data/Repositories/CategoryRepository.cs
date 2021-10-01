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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; } //Product tablosuna erişmek için dependency injection yaptık
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetWithProductByIdAsync(int categoryId)
        {
            var category = await appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x=> x.Id == categoryId);
            return category;
        }
    }
}
