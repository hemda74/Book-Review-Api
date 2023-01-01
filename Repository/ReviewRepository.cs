using AutoMapper;
using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;


        public ReviewRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Review> CreateReview(Review review)
        {
            var result = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    
        // delete review method 
        public async Task DeleteReview(int reviewId)
        {
            var result = await _context.Reviews
                      .FirstOrDefaultAsync(e => e.ReviewId == reviewId);
            if (result != null)
            {
                _context.Reviews.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        //method to return review by id 
        public async Task<Review?> GetReview(int reviewId)
        {
            // check the validation of  id
            if (reviewId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewId));
            }

            return await _context.Reviews
                .FirstOrDefaultAsync(e => e.ReviewId == reviewId);
        }
        // method to return all reviews 
        public async Task<IEnumerable<Review>> GetReviews()
        {
           return await _context.Reviews.ToListAsync();
        }
        // method to return reviews of book by book id 
        public async Task<IEnumerable<Review>?> GetReviewsOfABook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }
            return (await _context.Books.Include(b => b.BookId).FirstOrDefaultAsync(p => p.BookId == bookId))?.Reviews;
        }
        // method to check if review exists 
        public async Task<bool> ReviewExists(int reviewId)
        {
            // check the validation of author id
            if (reviewId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewId));
            }
            return await _context.Reviews.AnyAsync(o => o.ReviewId == reviewId);
        }
        // method to update review 
        public async Task<Review> UpdateReview(Review review)
        {
            if (review.ReviewId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(review));
            }
            var result = await _context.Reviews
            .FirstOrDefaultAsync(e => e.ReviewId == review.ReviewerId);

            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(review));
            }
            result.Title = review.Title;
            result.Text = review.Text;
            result.Rating = review.Rating;
            review.ReviewerId = review.ReviewerId;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}