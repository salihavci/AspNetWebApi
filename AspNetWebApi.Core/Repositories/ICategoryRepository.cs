using AspNetWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Core.Repositories
{
    /// <summary>
    /// Custom Category Repository
    /// </summary>
    public interface ICategoryRepository: IRepository<Category>
    {
        /// <summary>
        /// Getter with CategoryId
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <returns>Category And Product List</returns>
        Task<Category> GetWithProductByIdAsync(int categoryId);
    }
}
