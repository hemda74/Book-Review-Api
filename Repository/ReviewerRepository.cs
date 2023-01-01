using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using System.Net;

namespace BookReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }
        // add new reviewer
        public async Task<Reviewer> CreateReviewer(Reviewer reviewer)
        {
            var result = await _context.Reviewers.AddAsync(reviewer);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // remove reviewer method 
        public async Task DeleteReviewer(int reviewerId)
        {
            var result = await _context.Reviewers
                   .FirstOrDefaultAsync(e => e.ReviewerId == reviewerId);
            if (result != null)
            {
                _context.Reviewers.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        // method to return reviewer by id 
        public async Task<Reviewer?> GetReviewer(int reviewerId)
        {
            // check the validation of reviewer id
            if (reviewerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewerId));
            }

            return await _context.Reviewers
                .FirstOrDefaultAsync(e => e.ReviewerId == reviewerId);
        }

        public async Task<IEnumerable<Reviewer>> GetReviewers()
        {
           return await _context.Reviewers.ToListAsync();
        }

        public async Task<IEnumerable<Review>?> GetReviewsByReviewer(int reviewerId)
        {
            if (reviewerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewerId));
            }
            // "include()" method to work with virtual props for other classes  
            return (await _context.Reviewers.Include(b => b.Reviews).FirstOrDefaultAsync(p => p.ReviewerId == reviewerId))?.Reviews;
        }
        // check if reviewer exsits or not
        public async Task<bool> ReviewerExists(int reviewerId)
        {
            if (reviewerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewerId));
            }
            return await _context.Reviewers.AnyAsync(o => o.ReviewerId == reviewerId);
        }
        // update reviewer
        public async Task<Reviewer> UpdateReviewer(Reviewer reviewer)
        {
            
                if (reviewer.ReviewerId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(reviewer));
                }
                var result = await _context.Reviewers
                .FirstOrDefaultAsync(e => e.ReviewerId == reviewer.ReviewerId);

                if (result == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(reviewer));
                }
                result.FirstName = reviewer.FirstName;
                result.LastName = reviewer.LastName;
                await _context.SaveChangesAsync();
                return result;
            }
        
    }
}
