using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewerRepository reviewerRepository;
        private readonly IBookRepository bookRepository;

        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IBookRepository bookRepository)
        {
            this.reviewRepository = reviewRepository;
            this.reviewerRepository = reviewerRepository;
            this.bookRepository = bookRepository;
        }
        // handel get all reviews 
        [HttpGet]
        public async Task<ActionResult> GetReviews()
        {
            try
            {
                return Ok(await reviewRepository.GetReviews());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handel get reviewer by id method 
        [HttpGet("{reviewid:int}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                var result = await reviewRepository.GetReview(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // check if review exists or not 
        [HttpGet("{reviewid:int}/Exists ")]
        public async Task<ActionResult<Review>> ReviewExists(int id)
        {
            try
            {
                var result = await reviewRepository.ReviewExists(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handle get reviews of abook
        [HttpGet("book/{revId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfABook(int revId)
        {
            var reviews = reviewRepository.GetReviewsOfABook(revId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }
        // handel delete method
        [HttpDelete("{reviewId:int}/deletereview")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            try
            {
                var authorDelete = await reviewRepository.ReviewExists(id);

                if (authorDelete == null)
                {
                    return NotFound($"Reviewer with Id = {id} not found");
                }

                return await reviewRepository.DeleteReview(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        // handle update method
        [HttpPut("{reviewid:int}/updatereview")]
        public async Task<ActionResult<Review>> UpdateReview(int id, Review auth)
        {
            try
            {
                // check for id first
                if (id != auth.ReviewId)
                    return BadRequest("Review ID mismatch");
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var authorToUpdate = await reviewRepository.ReviewExists(id);

                if (authorToUpdate == null)
                    return NotFound($"Author with Id = {id} not found");

                return await reviewRepository.UpdateReview(auth);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        // handel create new author method
        [HttpPost("createreview")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
        {
            try
            {

                if (review == null)
                    return BadRequest();
                // Add custom model validation error method
                var authid = reviewRepository.ReviewExists(review.ReviewId);
                if (authid != null)
                {
                    ModelState.AddModelError("ReviewId", "Review Id already in use ");
                    return BadRequest(ModelState);
                }
                var createdReview = await reviewRepository.CreateReview(review);

                return CreatedAtAction(nameof(GetReview),
                    new { id = createdReview.ReviewId }, createdReview);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new author record");
            }
        }
        // Added missing delete range of reviews by a reviewer **>CK
        [HttpDelete("/DeleteReviewsByReviewer/{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
    public IActionResult DeleteReviewsByReviewer(int reviewerId)
        {
            //if (!reviewerRepository.ReviewerExists(reviewerId))
            //    return NotFound();

            var reviewsToDelete = reviewerRepository.GetReviewsByReviewer(reviewerId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!reviewRepository.DeleteReviews(reviewsToDelete))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
