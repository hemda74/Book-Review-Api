using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IBookRepository bookRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.bookRepository = bookRepository;
        }
        // handel get all authors 
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await categoryRepository.GetCategories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        // handel get author by id method 
        [HttpGet("{categoryid:int}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var result = await categoryRepository.GetCategory(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{categoryid:int}")]
        public async Task<ActionResult<Category>> CategoryExists(int id)
        {
            try
            {
                var result = await categoryRepository.CategoryExists(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // check this please 
        [HttpGet("{bookid:int}/category")]
        public IActionResult GetBooksOfCategory(int bookId)
        {
            var res = categoryRepository.CategoryExists(bookId);
            if (res == null)
            {
                return NotFound();

            }
            else
            {
                // Aly -> This is not right, naming conventions are wrong, mapping parameters are incorrect
                // Ahmed -> Handled Change AuthorExists To getbookbyauthor
                var books = bookRepository.GetBookById(bookId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(books);
            }
        }
            [HttpPost("createcategory")]
            public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
            {
                try
                {

                    if (category == null)
                        return BadRequest();
                    // Add custom model validation error method
                    var catid = categoryRepository.CategoryExists(category.CategoryId);
                    if (catid != null)
                    {
                        ModelState.AddModelError("CategoryId", "Category Id already in use ");
                        return BadRequest(ModelState);
                    }
                    var createdCat = await categoryRepository.CreateCategory(category);

                    return CreatedAtAction(nameof(GetCategoryById),
                        new { id = createdCat.CategoryId }, createdCat);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error creating new category record");
                }
            }
            // handle update method
            [HttpPut("{categoryid:int}/updatecategory")]
            public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
            {
                try
                {
                    // check for id first
                    if (id != category.CategoryId)
                        return BadRequest("Category ID mismatch");
                    // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                    var catToUpdate = await categoryRepository.CategoryExists(id);

                    if (catToUpdate == null)
                        return NotFound($"Category with Id = {id} not found");

                    return await categoryRepository.UpdateCategory(category);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating data");
                }
            }
            // handel delete method
            [HttpDelete("{category:int}/delete")]
            public async Task<ActionResult<Category>> DeleteCategory(int id)
            {
                try
                {
                    // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                    var catDelete = await categoryRepository.CategoryExists(id);

                    if (catDelete == null)
                    {
                        return NotFound($"Category with Id = {id} not found");
                    }

                    return await categoryRepository.DeleteCategory(id);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error deleting data");
                }
            }
   }
}


