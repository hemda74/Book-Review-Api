using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookReviewApp.Repository
// re implemntation of category repo if you need to review old one you will find it under new code commented  
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        // implement category exxsits method
        public async Task<bool> CategoryExists(int id)
        {
            // check the validation of id 
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return await _context.Categories.AnyAsync(o => o.CategoryId == id);


        }
        // implement create category method 
        public async Task<Category> CreateCategory(Category category)
        {
            var result = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // handle delete category method 
        public async Task DeleteCategory(int categoryid)
        {
            var result = await _context.Categories
                   .FirstOrDefaultAsync(e => e.CategoryId == categoryid);
            if (result != null)
            {
                _context.Categories.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        //Ahmed---> please check this method 
        // remove bookcategoris table and make the relation between books and categories direct relation and add asyncrouns programming  
        public async Task<IEnumerable<Book>?> GetBooksOfCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId));
            }
            return (await _context.Categories.Include(b => b.Books).FirstOrDefaultAsync(p => p.CategoryId == categoryId))?.Books;
        }
        // implemnt get all gategories method 
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();

        }
        // implemnt get category by id 
        public async Task<Category?> GetCategory(int id)
        {
            {
                // validate the id 
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                return await _context.Categories
                    .FirstOrDefaultAsync(e => e.CategoryId == id);
            }
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            {
                if ( category.CategoryId<= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(category));
                }
                var result = await _context.Categories
                .FirstOrDefaultAsync(e => e.CategoryId == category.CategoryId);

                if (result == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(category));
                }
                result.Name = category.Name;
                await _context.SaveChangesAsync();
                return result;
            }
        }
    }
}
