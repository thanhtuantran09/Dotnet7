using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Provider
{
    public class CategoryProvider
    {
        private readonly AppDbContext _dbContext;

        public CategoryProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetDataAsync()
        {
            return await _dbContext.categories.Include(c => c.products).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var category = await _dbContext.categories.Include(c => c.products).FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        public async Task CreateAsync(Category category)
        {
            _dbContext.categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.categories.Include(c => c.products).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                // Category not found
                return;
            }

            if (category.products != null && category.products.Any())
            {
                // Category is in use by products, return a response indicating it cannot be deleted
                // Adjust the response format as needed
                throw new InvalidOperationException("Category is in use and cannot be deleted.");
            }

            _dbContext.categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
