using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        Task<IEnumerable<Reviewer>> GetReviewers();
        Task<Reviewer> GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        Task<IEnumerable<Reviewer>> ReviewerExists(int reviewerId);
        Task<Reviewer> CreateReviewer(Reviewer reviewer);
        Task<Reviewer> UpdateReviewer(Reviewer reviewer);
        Task<Reviewer> DeleteReviewer(int reviewerId);
       
    }
}
