using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        ICollection<Book> GetBooksOfCategory(int categoryId);
        Task<IEnumerable<Category>> CategoryExists(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int categoryid);
    }
}
