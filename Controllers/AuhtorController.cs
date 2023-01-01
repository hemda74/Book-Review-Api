using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace BookReviewApp.Controllers
{
    // author conttroler
    // 1- add asyncrouns programming 
    // 2- fix query db twice
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // handel get all authors 
        [HttpGet]
        public async Task<ActionResult> GetAuthors()
        {
            try
            {
                return Ok(await authorRepository.GetAuthors());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handel get author by id method 
        [HttpGet("{authorid:int}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            try
            {
                var result = await authorRepository.GetAuthorById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // check if author exsists 
        [HttpGet("{authorid:int}/exsists")]
        public async Task<ActionResult<bool>> AuthorExists(int id)
        {
            try
            {
                var result = await authorRepository.AuthorExists(id);

                if (!result ) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // get book by author method
        [HttpGet("{authorid:int}/book")]
        public async Task<ActionResult<Book>> GetBookByAuthor(int authorId)
       {
            // Aly -> This method is totally wrong, you are querying database twice, while it's needed
            // Ahmed ---> try to fix 
            try
            {
                // final 
                var book = await authorRepository.GetBookByAuthor(authorId);
                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
                
         }
        [HttpGet("{authorid:int}/country")]
        public async Task<ActionResult<Country>> GetCountryByAuthor(int authorId)
        {
            try
            {
                var book = await authorRepository.GetCountryByAuthor(authorId);
                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }

        }
        // handel create new author method
        [HttpPost("createauthor")]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            try
            {
                
                if (author == null)
                    return BadRequest();
               
                var createdAuthor = await authorRepository.CreateAuthor(author);

                return CreatedAtAction(nameof(GetAuthorById),
                    new { id = createdAuthor.AuthorId }, createdAuthor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new author record");
            }
        }
        // handle update method
        [HttpPut("{authorid:int}/updateauthor")]
        public async Task<ActionResult<Author>> UpdateAuthor( Author author)
        {
            try
            {
                // check for the entered data first
                if (author.AuthorId <= 0|| author==null )
                    return BadRequest("Author Id not found ");

                return await authorRepository.UpdateAuthor(author);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        // handel delete method
        [HttpDelete("{authorid:int}/deleteauthor")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try 
            { 
                if (id <=0 )
                {
                    return BadRequest($"Author with Id = {id} not found");
                }

                await authorRepository.DeleteAuthor(id);
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
    

