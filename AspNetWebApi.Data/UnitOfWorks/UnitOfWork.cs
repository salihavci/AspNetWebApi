using AspNetWebApi.Core.Repositories;
using AspNetWebApi.Core.UnitOfWorks;
using AspNetWebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        public IProductRepository product => _productRepository = _productRepository ?? new ProductRepository(_context); //Product repository varsa üsttekini al , eğer null ise yeni productrepository oluştur

        public ICategoryRepository category => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
