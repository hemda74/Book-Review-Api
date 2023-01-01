using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    // changes in reviewers
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviews();
        Task<Review?> GetReview(int reviewId);
        Task<IEnumerable<Review>?> GetReviewsOfABook(int bookId);
        Task<bool> ReviewExists(int reviewId);
        Task<Review> CreateReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task DeleteReview(int reviewId);
        
        
    }
}
