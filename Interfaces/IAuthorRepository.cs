using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        // may return null 
        Task<Author?> GetAuthorById (int authorId);
        Task<Author?> GetAuthorOfABook(int bookId);
        Task<IEnumerable<Book>?> GetBookByAuthor(int authorId);
        Task<IEnumerable<Country>?> GetCountryByAuthor(int authorId);
        Task<bool> AuthorExists(int authorId);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task DeleteAuthor(int authorId);
        
    }
}
