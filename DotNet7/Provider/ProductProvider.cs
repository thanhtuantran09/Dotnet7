// Providers/ProductProvider.cs
using DotNet7.Data;
using DotNet7.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNet7.Provider
{
    public class ProductProvider
    {
        private readonly AppDbContext _dbContext;

        public ProductProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetDataAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}