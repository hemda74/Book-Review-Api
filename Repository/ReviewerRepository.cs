using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }
        // handle create reviwer  
        public async Task<Reviewer> CreateReviewer(Reviewer reviewer)
        {
            var result = await _context.Reviewers.AddAsync(reviewer);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // handle delete method 
        public async Task<Reviewer> DeleteReviewer(int reviewerId)
        {
            {
                var result = await _context.Reviewers
                    .FirstOrDefaultAsync(e => e.ReviewerId == reviewerId);
                if (result != null)
                {
                    _context.Reviewers.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
        }
        // handel get reviewer by Id
        public async Task<Reviewer> GetReviewer(int reviewerId)
        {
            return await _context.Reviewers
                .FirstOrDefaultAsync(e => e.ReviewerId == reviewerId);
        }
        // handle get all Reviewers
        public async Task<IEnumerable<Reviewer>> GetReviewers()
        {
            return await _context.Reviewers.ToListAsync();
        }
        // Ahmed -----------> please implement this method correctly
        //// GetReviewsByReviewer method 
        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Reviewer>> ReviewerExists(int reviewerId)
        {

            IQueryable<Reviewer> query = _context.Reviewers;

            if (reviewerId != null)
            {
                query = query.Where(e => e.ReviewerId == reviewerId);
            }

            return await query.ToListAsync();

        }

        public async Task<Reviewer> UpdateReviewer(Reviewer reviewer) 
        { 
        var result = await _context.Reviewers
          .FirstOrDefaultAsync(e => e.ReviewerId == reviewer.ReviewerId);

            if (result != null)
            {
                result.ReviewerId = reviewer.ReviewerId;
                result.FirstName = reviewer.FirstName;
                result.LastName = reviewer.LastName;
                

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
//////////////////////////////////////////////////////
//                    old reviewer repo
//////////////////////////////////////////////////////
//        public bool CreateReviewer(Reviewer reviewer)
//        {
//            _context.Add(reviewer);
//            return Save();
//        }

//        public bool DeleteReviewer(Reviewer reviewer)
//        {
//            _context.Remove(reviewer);
//            return Save();
//        }

//        public Reviewer GetReviewer(int reviewerId)
//        {
//            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
//        }

//        public ICollection<Reviewer> GetReviewers()
//        {
//            return _context.Reviewers.ToList();
//        }

//        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
//        {
//            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
//        }

//        public bool ReviewerExists(int reviewerId)
//        {
//            return _context.Reviewers.Any(r => r.Id == reviewerId);
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public bool UpdateReviewer(Reviewer reviewer)
//        {
//            _context.Update(reviewer);
//            return Save();
//        }
//    }
//}
