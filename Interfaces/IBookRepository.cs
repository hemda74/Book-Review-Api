using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IBookRepository
    {
        //----Ahmed-> change to add async programming 
        Task<IEnumerable<Book>> GetBooks();
        Task<Book?> GetBookById(int id);
        Task<Book?> GetBookByName(string name);
        Task<decimal?> GetBookRating(int bookId);
        Task<bool> BookExists(int bookId);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook( Book book);
        Task DeleteBook(int bookId);
        
    }
}
