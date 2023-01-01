using BookReviewApp.Data;

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
        // re implementation of book exists 
        // check if book exists or not       
        public async Task<bool> BookExists(int bookId)
        {
            // check the validation of book id
            if (bookId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }
            return await _context.Books.AnyAsync(o => o.BookId == bookId);


        }
        //  create book method  
        public async Task<Book> CreateBook(Book book)
        {
            var result = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteBook(int bookId)
        {
            var result = await _context.Books
                .FirstOrDefaultAsync(e => e.BookId == bookId);
            if (result != null)
            {
                _context.Books.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        // get book by id method 
        public async Task<Book?> GetBookById(int bookId)
        {
            // check the validation of book id
            if (bookId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }

            return await _context.Books
                .FirstOrDefaultAsync(e => e.BookId == bookId);
        }

        public async Task<Book?> GetBookByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            return await _context.Books
                .FirstOrDefaultAsync(e => e.BookName == name);
        }
        // Ahmed -> is that method right ? if not please handel it 
        // fail first
        public async Task<decimal?> GetBookRating(int bookId)
        {
            // check the validation of book id
            if (bookId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }
            var review = await _context.Reviews.Where(p => p.Book.BookId == bookId).ToListAsync();

            if (review.Count() > 0)
                return ((int)review.Sum(r => r.Rating) / review.Count());

            return 0;
        }
        // method return all books 
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        // method to updatebook
        public async Task<Book> UpdateBook(Book book)
        {
            if (book.BookId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(book));
            }
            var result = await _context.Books
            .FirstOrDefaultAsync(e => e.BookId == book.BookId);
            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(book));
            }
            result.BookName = book.BookName;
            result.ReleaseDateUTC = book.ReleaseDateUTC;
            result.Categories = book.Categories;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}