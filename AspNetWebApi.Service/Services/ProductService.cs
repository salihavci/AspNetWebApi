using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Repositories;
using AspNetWebApi.Core.Services;
using AspNetWebApi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Service.Services
{
    public class ProductService : Service<Product>,IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Product> GetWithCategoryByIdAsync(int id)
        {
            return await _unitOfWork.product.GetWithCategoryByIdAsync(id);
        }
    }
}
