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
        // create review method 
        public async Task<Review> CreateReview(Review review)
        {
            var result = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Review> DeleteReview(int reviewId)
        {
            {
                var result = await _context.Reviews
                    .FirstOrDefaultAsync(e => e.ReviewId == reviewId);
                if (result != null)
                {
                    _context.Reviews.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
                       return save();
        }

        public async Task<Review> GetReview(int reviewId)
        {
            return await _context.Reviews
                    .FirstOrDefaultAsync(e => e.ReviewId == reviewId);
        }

        public async  Task<IEnumerable<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();

        }
        // Ahmed -----> please check this method 
        // 
        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
            return _context.Reviews.Where(r => r.Book.BookId == bookId).ToList();
        }
        // check if review exsits or not 
        public async Task<IEnumerable<Review>> ReviewExists(int reviewId)
        {
            IQueryable<Review> query = _context.Reviews;

            if (reviewId != null)
            {
                query = query.Where(e => e.ReviewId == reviewId);
            }

            return await query.ToListAsync();
        }
        // update review method 
        public async Task<Review> UpdateReview(Review review)
        {
            var result = await _context.Reviews
          .FirstOrDefaultAsync(e => e.ReviewId == review.ReviewId);

            if (result != null)
            {
                result.ReviewId = review.ReviewId;
                result.Title = review.Title;
                result.Text= review.Text;
                result.Rating= review.Rating;
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
//        public bool CreateReview(Review review)
//        {
//            _context.Add(review);
//            return Save();
//        }

//        public bool DeleteReview(Review review)
//        {
//           _context.Remove(review);
//            return Save();
//        }

//        public bool DeleteReviews(List<Review> reviews)
//        {
//            _context.RemoveRange(reviews);
//            return Save();
//        }

//        public Review GetReview(int reviewId)
//        {
//            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
//        }

//        public ICollection<Review> GetReviews()
//        {
//            return _context.Reviews.ToList();
//        }

//        public ICollection<Review> GetReviewsOfABook(int bookId)
//        {
//            return _context.Reviews.Where(r => r.Book.Id == bookId).ToList();
//        }

//        public ICollection<Review> GetReviewsOfAPokemon(int bookId)
//        {
//            return _context.Reviews.Where(r => r.Book.Id == bookId).ToList();
//        }

//        public bool ReviewExists(int reviewId)
//        {
//            return _context.Reviews.Any(r => r.Id == reviewId);
//        }

//       

//        public bool UpdateReview(Review review)
//        {
//            _context.Update(review);
//            return Save();
//        }
//    }
//}
