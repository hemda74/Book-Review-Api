using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    // changes in reviewers
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviews();
        Task<Review> GetReview(int reviewId);
        ICollection<Review> GetReviewsOfABook(int bookId);
        Task<IEnumerable<Review>> ReviewExists(int reviewId);
        Task<Review> CreateReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task<Review> DeleteReview(int reviewId);
        bool DeleteReviews(List<Review> reviews);
        bool save();
    }
}
