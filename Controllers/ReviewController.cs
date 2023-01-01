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
     

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
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
        public async Task<ActionResult<Review>?> GetReview(int id)
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
        public async Task<ActionResult<Review>?> ReviewExists(int id)
        {
            try
            {
                var result = await reviewRepository.ReviewExists(id);

                if (!result) return NotFound();

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
        public async Task<ActionResult<Review>> GetReviewsOfABook(int revId)
        {
            try
            {
                var reviews = await reviewRepository.GetReviewsOfABook(revId);
                if (reviews == null) return NotFound();
                return Ok(reviews);
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handel delete method
        [HttpDelete("{reviewId:int}/deletereview")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            try
            {
                if (id<=0)
                {
                    return NotFound($"Reviewer with Id = {id} not found");
                }

                 await reviewRepository.DeleteReview(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        // handle update method
        [HttpPut("{reviewid:int}/updatereview")]
        public async Task<ActionResult<Review>> UpdateReview(Review rev)
        {
            try
            {
                // check for given data first
                if (rev==null|| rev.ReviewerId<=0)
                    return BadRequest("Review ID mismatch");
               
                 await reviewRepository.UpdateReview(rev);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        // handel create new review method
        [HttpPost("createreview")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
        {
            try
            {

                if (review == null)
                    return BadRequest();
             
                var createdReview = await reviewRepository.CreateReview(review);

                return CreatedAtAction(nameof(GetReview),
                    new { id = createdReview.ReviewId }, createdReview);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new review");
            }
        }
       
    }
}
