using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Net;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BookReviewApp.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        // check if author exists or not       
        public async Task<bool> AuthorExists(int authorId)
        {
            // check the validation of author id
            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }
            return  await _context.Authors.AnyAsync(o => o.AuthorId == authorId);
        }
        // get author by id method 
        public async Task<Author?> GetAuthorById(int authorId)
        {
            // check the validation of author id
            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }
            
            return await _context.Authors
                .FirstOrDefaultAsync(e => e.AuthorId == authorId);
        }
        // any virtual needs include 
        // final
        public async Task<Author?> GetAuthorOfABook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }
            // "include()" method to work with virtual props for other classes  
            return (await _context.Books.Include(b=>b.Author).FirstOrDefaultAsync(p => p.BookId == bookId))?.Author;
        }
        // method to return all authors 
        public async Task<IEnumerable<Author>> GetAuthors()
        {

            return await _context.Authors.ToListAsync();
        }
        // method that takes author record and add it to database 
        public async Task<Author> CreateAuthor(Author author)
        {
            var result = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // method to delete author record from database 
        public async Task DeleteAuthor(int authorId)
        {
            
                var result = await _context.Authors
                    .FirstOrDefaultAsync(e => e.AuthorId == authorId);
                if (result != null)
                {
                    _context.Authors.Remove(result);
                    await _context.SaveChangesAsync();
                }
                //return Task.CompletedTask;
            
        }
        // method return books from author take authorid and return books  
        public async Task<IEnumerable<Book>?> GetBookByAuthor(int authorId)
        {

            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }
            return (await _context.Authors.Include(b => b.Books).FirstOrDefaultAsync(p => p.AuthorId == authorId))?.Books;
        }
        public async Task<IEnumerable<Country>?> GetCountryByAuthor(int authorId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }
            return (await _context.Authors.Include(b => b.Country).FirstOrDefaultAsync(p => p.AuthorId == authorId))?.Country;
        }
        // method to update author record 
        public async Task<Author> UpdateAuthor(Author author)
        {        
            if (author.AuthorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(author));
            }
            var result = await _context.Authors
            .FirstOrDefaultAsync(e => e.AuthorId == author.AuthorId);

            if (result == null) {
                throw new ArgumentOutOfRangeException(nameof(author));
            }
                result.FirstName = author.FirstName;
                result.LastName = author.LastName;
                result.Gym = author.Gym;
                result.Country = author.Country;
                await _context.SaveChangesAsync();
                return result;
        }
    }
}
