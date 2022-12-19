using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IAuthorRepository
    {
        // Ahmed Change Get Author -> Get Authors
        ICollection<Author> GetAuthors();
        Author GetAuthor (int authorId);
        ICollection<Author> GetAuthorOfBook(int bookId);
        ICollection<Book> GetBookByAuthor(int athourId);
        bool AuthorExists(int authorId);
        bool CreateAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool Save();
    }
}
