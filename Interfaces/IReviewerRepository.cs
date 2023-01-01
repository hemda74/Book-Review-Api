using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        Task<IEnumerable<Reviewer>> GetReviewers();
        Task<Reviewer?> GetReviewer(int reviewerId);
        Task<IEnumerable<Review>?> GetReviewsByReviewer(int reviewerId);
        Task<bool> ReviewerExists(int reviewerId);
        Task<Reviewer> CreateReviewer(Reviewer reviewer);
        Task<Reviewer> UpdateReviewer(Reviewer reviewer);
        Task DeleteReviewer(int reviewerId);
        
    }
}
