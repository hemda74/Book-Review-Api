using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// new controller base 
namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository ReviewersRepository;
        private readonly IReviewRepository reviewRepository;

        public ReviewerController(IReviewerRepository ReviewersRepository)
        {
            this.ReviewersRepository = ReviewersRepository;
        }
        // handel get all reviewer 
        [HttpGet]
        public async Task<ActionResult> GetReviewers()
        {
            try
            {
                return Ok(await ReviewersRepository.GetReviewers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handel get reviewer by id method 
        [HttpGet("{reviewerid:int}")]
        public async Task<ActionResult<Reviewer>> GetReviewer(int id)
        {
            try
            {
                var result = await ReviewersRepository.GetReviewer(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{reviewerid:int}")]
        public async Task<ActionResult<Reviewer>> ReviewerExists(int id)
        {
            try
            {
                var result = await ReviewersRepository.ReviewerExists(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        public IReviewerRepository GetReviewersRepository()
        {
            return ReviewersRepository;
        }

        // handle update method
        [HttpPut("{authorid:int}/updateauthor")]
        public async Task<ActionResult<Reviewer>> UpdateReviewer(int id, Reviewer auth )
        {
            try
            {
                // check for id first
                if (id != auth.ReviewerId)
                    return BadRequest("Author ID mismatch");
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var authorToUpdate = await ReviewersRepository.ReviewerExists(id);

                if (authorToUpdate == null)
                    return NotFound($"Author with Id = {id} not found");

                return await ReviewersRepository.UpdateReviewer(auth);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        // handel delete method
        [HttpDelete("{authorid:int}/delete")]
        public async Task<ActionResult<Reviewer>> DeleteReviewer(int id)
        {
            try
            {
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var authorDelete = await ReviewersRepository.ReviewerExists(id);

                if (authorDelete == null)
                {
                    return NotFound($"Reviewer with Id = {id} not found");
                }

                return await ReviewersRepository.DeleteReviewer(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
    



    } 
}
