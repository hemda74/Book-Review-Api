using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateAuthor(Author author)
        {
            _context.Add(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Remove(author);
            return Save();
        }

        public Author GetAuthor(int authorId)
        {
            return _context.Authors.Where(o => o.Id == authorId).FirstOrDefault();
        }

        public ICollection<Author> GetAuthorOfBook(int bookId)
        {
            return _context.BookAuthors.Where(p => p.Book.Id == bookId).Select(o => o.Author).ToList();
        }

        public ICollection<Author> GetAuthros()
        {
            return _context.Authors.ToList();
        }

        public ICollection<Book> GetBookByAuthor(int ownerId)
        {
            return _context.BookAuthors.Where(p => p.Author.Id == ownerId).Select(p => p.Book).ToList();
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(o => o.Id == authorId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAuthor(Author author)
        {
            _context.Update(author);
            return Save();
        }

        // Aly -> Should be named GetAuthor(s) because it returns list
        // Ahmed Handled Change GetAuthor Method to GetAuthors Method in both Author Interface And Author Repo
        public ICollection<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }
    }
}
