using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookReviewApp.Controllers
{
    // author conttroler
    // 1- add asyncrouns programming 
    // 2- try to fix query db twice
    // plaese look at  --> changes in author exists method 
    // i can't find any value of DTO so i stoped working with it  
    // you will find old author controller commented if it need to be reviewed  
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private const string craeteapi = "createauthor";
        private readonly IAuthorRepository authorRepository;
        private readonly ICountryRepository countryRepository;
     

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
        /////////////////////////////////////////////////
        // Ahmed  is that method right or how should i code it ?
        [HttpGet("{authorid:int}")]
        public async Task<ActionResult<Author>> AuthorExists(int id)
        {
            try
            {
                var result = await authorRepository.AuthorExists(id);

                if (result == null) return NotFound();

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
        public IActionResult GetBookByAuthor(int authorId)
       {
            // Aly -> This method is totally wrong, you are querying database twice, while it's needed
            // Ahmed ---> try to fix 
            var res = authorRepository.AuthorExists(authorId);
            if (res == null)
            {
                return NotFound();

            }
            else
            {
                // Aly -> This is not right, naming conventions are wrong, mapping parameters are incorrect
                // Ahmed -> Handled Change AuthorExists To getbookbyauthor
                var author = authorRepository.GetBookByAuthor(authorId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(author);
            }
        }
        // handel create new author method
        [HttpPost(craeteapi)]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            try
            {
                
                if (author == null)
                    return BadRequest();
                // Add custom model validation error method
                var authid = authorRepository.AuthorExists(author.AuthorId);
                if (authid != null)
                {
                    ModelState.AddModelError("AuthorId", "Author Id already in use ");
                    return BadRequest(ModelState);
                }
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
        public async Task<ActionResult<Author>> UpdateAuthor(int id, Author author)
        {
            try
            {
                // check for id first
                if (id != author.AuthorId)
                    return BadRequest("Author ID mismatch");
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var authorToUpdate = await authorRepository.AuthorExists(id);

                if (authorToUpdate == null)
                    return NotFound($"Author with Id = {id} not found");

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
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            try
            {
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var authorDelete = await authorRepository.AuthorExists(id);

                if (authorDelete == null)
                {
                    return NotFound($"Author with Id = {id} not found");
                }

                return await authorRepository.DeleteAuthor(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
    

