using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<IEnumerable<Book>?> GetBooksOfCategory(int categoryId);
        Task<bool> CategoryExists(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int categoryid);
    }
}
