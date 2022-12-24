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

        public  async Task<IEnumerable<Category>> CategoryExists(int id)
        {

            IQueryable<Category> query = _context.Categories;

            if (id != null)
            {
                query = query.Where(e => e.CategoryId == id);
            }

            return await query.ToListAsync();

        }

        public async Task<Category> CreateCategory(Category category)
        {
            var result = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteCategory(int categoryid)
        {
            {
                var result = await _context.Categories
                    .FirstOrDefaultAsync(e => e.CategoryId == categoryid);
                if (result != null)
                {
                    _context.Categories.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
        }


        // Ahmed --->please look at this method if it incorret please correct it 
        public ICollection<Book> GetBooksOfCategory(int categoryId)
        {
            return _context.BookCategories.Where(p => p.CategoryId == categoryId).Select(o => o.Book).ToList();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories
                  .FirstOrDefaultAsync(e => e.CategoryId == id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            {
                var result = await _context.Categories
              .FirstOrDefaultAsync(e => e.CategoryId == category.CategoryId);

                if (result != null)
                {
                    result.CategoryId = category.CategoryId;
                    result.Name = category.Name;
                    await _context.SaveChangesAsync();

                    return result;
                }

                return null;
            }
        }
    }
}
//        public bool CategoryExists(int id)
//        {
//            return _context.Categories.Any(c => c.Id == id);
//        }

//        public bool CreateCategory(Category category)
//        {
//            _context.Add(category);
//            return Save();
//        }

//        public bool DeleteCategory(Category category)
//        {
//            _context.Remove(category);
//            return Save();
//        }

//        public ICollection<Book> GetBookByCategory(int categoryId)
//        {
//            return _context.BookCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Book).ToList();
//        }

//        public ICollection<Category> GetCategories()
//        {
//            return _context.Categories.ToList();
//        }

//        public Category GetCategory(int id)
//        {
//            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
//        }

//        public ICollection<Book> GetPokemonByCategory(int categoryId)
//        {
//            return _context.BookCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Book).ToList();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public bool UpdateCategory(Category category)
//        {
//            _context.Update(category);
//            return Save();
//        }
//    }
//}
