using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
// new controller base 
namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository reviewersRepository;
        

        public ReviewerController(IReviewerRepository reviewersRepository)
        {
            this.reviewersRepository = reviewersRepository;
        }
        // handel get all reviewer 
        [HttpGet]
        public async Task<ActionResult> GetReviewers()
        {
            try
            {
                return Ok(await reviewersRepository.GetReviewers());
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
                var result = await reviewersRepository.GetReviewer(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{reviewerid:int}/Exists ")]
        public async Task<ActionResult<bool>> ReviewerExists(int id)
        {
            try
            {
                var result = await reviewersRepository.ReviewerExists(id);

                if (!result) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // handle update method
        [HttpPut("{authorid:int}/updateauthor")]
        public async Task<ActionResult<Reviewer>> UpdateReviewer( Reviewer rever)
        {
            try
            {
                // check for the entered data first
                if (rever.ReviewerId <= 0 || rever == null)
                    return BadRequest("Reviewer Id not found ");

                return await reviewersRepository.UpdateReviewer(rever);
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
                if (id <= 0)
                {
                    return BadRequest($"Reviewer with Id = {id} not found");
                }

                await reviewersRepository.DeleteReviewer(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
    