using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookReviewApp.Controllers
{
    // category contoller 
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
       

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
           
        }
        // handel get all categories
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
        // handel get Category by id method 
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
        [HttpGet("{categoryid:int}/Exists ")]
        public async Task<ActionResult<Category>> CategoryExists(int id)
        {
            try
            {
                var result = await categoryRepository.CategoryExists(id);

                if (!result ) return NotFound();

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
            try
            {
                var res = categoryRepository.GetBooksOfCategory(bookId);
                if (res==null)
                    return NotFound();

                return Ok(res);
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                         "Error retrieving data from the database");
            }    
            

            
        }
        // implement create category method 
        [HttpPost("createcategory")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest();

                var createdcat = await categoryRepository.CreateCategory(category);
                return CreatedAtAction(nameof(GetCategoryById),
                    new { id = createdcat.CategoryId }, createdcat);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new author record");
            }
        }
        // handle update method
        [HttpPut("{categoryid:int}/updatecategory")]
        public async Task<ActionResult<Category>> UpdateCategory( Category category)
        {
            {
                try
                {
                    if (category == null)
                        return BadRequest($"category not found ");

                    return await categoryRepository.UpdateCategory(category);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating data");
                }
            }
        }
        // handel delete method
        [HttpDelete("{category:int}/deletecategory")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest($"category with Id = {id} not found");
                }

                await categoryRepository.DeleteCategory(id);
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


