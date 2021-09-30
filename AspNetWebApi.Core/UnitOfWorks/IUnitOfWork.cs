using AspNetWebApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository product { get; }
        ICategoryRepository category { get; }
        void Commit();
        Task CommitAsync();
    }
}
