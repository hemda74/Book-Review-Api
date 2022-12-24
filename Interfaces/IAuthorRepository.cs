using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IAuthorRepository
    {
        // Ahmed Change Get Author -> Get Authors
        // update interface to use async programming 
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById (int authorId);
        ICollection<Author> GetAuthorOfABook(int bookId);
        ICollection<Book> GetBookByAuthor(int authorId);
        Task<IEnumerable<Author>> AuthorExists(int authorId);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<Author> DeleteAuthor(int authorId);
        
    }
}
