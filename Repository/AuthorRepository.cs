using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

namespace BookReviewApp.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        // re implementation of authorexists  
        public async Task<IEnumerable<Author>> AuthorExists(int authorId)
        {
          
            IQueryable<Author> query = _context.Authors;

            if (authorId != null)
            {
                query = query.Where(e => e.AuthorId == authorId);
            }

            return await query.ToListAsync();
            
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            return await _context.Authors
                .FirstOrDefaultAsync(e => e.AuthorId == authorId);
        }

        public ICollection<Author> GetAuthorOfABook(int bookId)
        {
            return _context.Books.Where(p => p.BookId == bookId).Select(o => o.Author).ToList();
        }
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> CreateAuthor(Author author)
        {
            var result = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Author> DeleteAuthor(int authorId)
        {
            {
                var result = await _context.Authors
                    .FirstOrDefaultAsync(e => e.AuthorId == authorId);
                if (result != null)
                {
                    _context.Authors.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
        }

        ICollection<Book> IAuthorRepository.GetBookByAuthor(int authorId) => _context.Authors.Where(p => p.AuthorId == authorId).Select(p => p.Book).ToList();

        public async Task<Author> UpdateAuthor(Author author)
        {
            var result = await _context.Authors
          .FirstOrDefaultAsync(e => e.AuthorId == author.AuthorId);

            if (result != null)
            {
                result.AuthorId = author.AuthorId;
                result.FirstName = author.FirstName;
                result.LastName = author.LastName;
                result.Gym = author.Gym;
                result.Country = author.Country;
                result.CountryId = author.CountryId;
                result.Book = author.Book;
                result.Book = author.Book;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
