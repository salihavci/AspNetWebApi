using AspNetWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int id);
        //bool ControlInnerBarcode(Product product) //Barkod kontrolü için harici bir fonksiyon yazılabilir.
    }
}
