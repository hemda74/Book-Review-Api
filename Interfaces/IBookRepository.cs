using BookReviewApp.Dto;
using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(int id);
        Book GetBook(string name);
        Book GetBookTrimToUpper(BookDto BookCreate);
        decimal GetBookRating(int bookId);
        bool BookExists(int bookId);
        bool CreateBook (int authorId, int categoryId, Book book);
        bool UpdateBook(int authorId, int categoryId, Book book);
        bool DeleteBook(Book book);
        bool Save();
    }
}
