using BookReviewApp.Data;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateBook(int authorId, int categoryId, Book book)
        {
            var bookAuthorEntity = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var bookAuthor = new BookAuthor()
            {
                Author = bookAuthorEntity,
                Book = book,
            };

            _context.Add(bookAuthor);

            var bookCategory = new BookCategory()
            {
                Category = category,
                Book = book,
            };

            _context.Add(bookCategory);

            _context.Add(book);

            return Save();
        }


        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(p => p.Id).ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(p => p.Id == id).FirstOrDefault();
        }

        public Book GetBook(string name)
        {
            return _context.Books.Where(p => p.Name == name).FirstOrDefault();
        }

        public Book GetBookTrimToUpper(BookDto BookCreate)
        {
            return GetBooks().Where(c => c.Name.Trim().ToUpper() == BookCreate.Name.TrimEnd().ToUpper())
               .FirstOrDefault();
        }

        public decimal GetBookRating(int bookId)
        {
            var review = _context.Reviews.Where(p => p.Book.Id == bookId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(p => p.Id == bookId);
        }

        public bool UpdateBook(int authorId, int categoryId, Book book)
        {
            _context.Update(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _context.Remove(book);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}