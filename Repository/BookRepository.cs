using BookReviewApp.Data;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookReviewApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }
        // re implementation of bookexists 
        public async Task<IEnumerable<Book>> BookExists(int bookId)
        {
            IQueryable<Book> query = _context.Books;

            if (bookId != null)
            {
                query = query.Where(e => e.BookId == bookId);
            }

            return await query.ToListAsync();
        }

        public async Task<Book> CreateBook(Book book)
        {
            var result = await _context.Books.AddAsync(book);

            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            {
                var result = await _context.Books
                    .FirstOrDefaultAsync(e => e.BookId == bookId);
                if (result != null)
                {
                    _context.Books.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
        }

        public async Task<Book> GetBookById(int bookid)
        {
            return await _context.Books
             .FirstOrDefaultAsync(e => e.BookId == bookid);
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _context.Books
             .FirstOrDefaultAsync(e => e.BookName == name);
        }
        // Ahmed -> is that method right ? if not please handel it 
        public async Task<decimal> GetBookRating(int bookId)
        {
            IQueryable <Review> review = _context.Reviews;
            if (bookId != null)
            {
                if (review.Count() > 0)
                    return ((int)review.Sum(r => r.Rating) / review.Count());
            }
            return 0;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }


        // Ahmed -> is that method right ? if not please handel it 

        public  async Task<Book> UpdateBook(Book book)
        {
            {
                var result = await _context.Books
              .FirstOrDefaultAsync(e => e.BookId == book.BookId);

                if (result != null)
                {
                    book.BookId = book.BookId;
                    result.BookName = book.BookName;
                    result.ReleaseDateUTC = book.ReleaseDateUTC;
                    await _context.SaveChangesAsync();

                    return result;
                }

                return null;
            }
        }

        //    public bool CreateBook(int authorId, int categoryId, Book book)
        //    {
        //        var bookAuthorEntity = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        //        var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

        //        var bookAuthor = new BookAuthor()
        //        {
        //            Author = bookAuthorEntity,
        //            Book = book,
        //        };

        //        _context.Add(bookAuthor);

        //        var bookCategory = new BookCategory()
        //        {
        //            Category = category,
        //            Book = book,
        //        };

        //        _context.Add(bookCategory);

        //        _context.Add(book);

        //        return Save();
        //    }


        //    public ICollection<Book> GetBooks()
        //    {
        //        return _context.Books.OrderBy(p => p.Id).ToList();
        //    }

        //    public Book GetBook(int id)
        //    {
        //        return _context.Books.Where(p => p.Id == id).FirstOrDefault();
        //    }

        //    public Book GetBook(string name)
        //    {
        //        return _context.Books.Where(p => p.Name == name).FirstOrDefault();
        //    }

        //    public Book GetBookTrimToUpper(BookDto BookCreate)
        //    {
        //        return GetBooks().Where(c => c.Name.Trim().ToUpper() == BookCreate.Name.TrimEnd().ToUpper())
        //           .FirstOrDefault();
        //    }

        //    public decimal GetBookRating(int bookId)
        //    {
        //        var review = _context.Reviews.Where(p => p.Book.Id == bookId);

        //        if (review.Count() <= 0)
        //            return 0;

        //        return ((decimal)review.Sum(r => r.Rating) / review.Count());
        //    }

        //    public bool BookExists(int bookId)
        //    {
        //        return _context.Books.Any(p => p.Id == bookId);
        //    }

        //    public bool UpdateBook(int authorId, int categoryId, Book book)
        //    {
        //        _context.Update(book);
        //        return Save();
        //    }

        //    public bool DeleteBook(Book book)
        //    {
        //        _context.Remove(book);
        //        return Save();
        //    }

        //    public bool Save()
        //    {
        //        var saved = _context.SaveChanges();
        //        return saved > 0 ? true : false;
        //    }

        //}
    }
}