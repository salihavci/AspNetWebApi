using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Repositories;
using AspNetWebApi.Core.Services;
using AspNetWebApi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Category> GetWithProductByIdAsync(int categoryId)
        {
            return await _unitOfWork.category.GetWithProductByIdAsync(categoryId);
        }
    }
}
