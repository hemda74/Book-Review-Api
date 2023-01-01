using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace BookReviewApp.Controllers
{
    // book conttroler
    namespace BookReviewApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BookController : ControllerBase
        {
            private readonly IBookRepository bookRepository;
            public BookController(IBookRepository bookRepository)
            {
                this.bookRepository = bookRepository;
            }
            // handel get all Books 
            [HttpGet]
            public async Task<ActionResult> GetBooks()
            {
                try
                {
                    return Ok(await bookRepository.GetBooks());
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }

            }
            // handel get book by id method 
            [HttpGet("{bookid:int}")]
            public async Task<ActionResult<Book>> GetBookById(int id)
            {
                try
                {
                    var result = await bookRepository.GetBookById(id);

                    if (result == null) return NotFound();

                    return result;
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }
            // handel get book by Name method 
            [HttpGet("{bookname}")]
            public async Task<ActionResult<Book>> GetBookByName(string name)
            {
                try
                {
                    var result = await bookRepository.GetBookByName(name);

                    if (result == null) return NotFound();

                    return result;
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }
            // handle book exsits
            [HttpGet("{bookid:int}/exists")]
            public async Task<ActionResult<Book>> BookExists(int id)
            {
                try
                {
                    var result = await bookRepository.BookExists(id);
                    // if res return null value it will return notfound 
                    if (!result) return NotFound();

                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }
            // get book rating 
            [HttpGet("{bookid:int}/rating")]
            public async Task<ActionResult<decimal?>> GetBookRating(int bookId)
            {
                try
                {
                    var res = await bookRepository.GetBookRating(bookId);
                    if (res == 0)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(res);
                    }
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }


            }
            // create method 
            [HttpPost("createbook")]
            public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
            {
                try
                {
                    if (book == null)
                        return BadRequest();
                    var createdBook = await bookRepository.CreateBook(book);
                    return CreatedAtAction(nameof(GetBookById),
                        new { id = createdBook.BookId }, createdBook);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error creating new book record");
                }
            }
            // handle update method
            [HttpPut("{bookid:int}/updatebook")]
            public async Task<ActionResult<Book>> UpdateAuthor( Book book)
            {
                try
                {
                    // check for id first
                    if (book == null)
                        return BadRequest($"Book ID mismatch");

                    return await bookRepository.UpdateBook(book);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating data");
                }
            }
            // handel delete method
            [HttpDelete("{bookid:int}/deletebook")]
            public async Task<ActionResult> DeleteBook(int id)
            {
                try
                {
                    if (id <= 0)
                    {
                        return NotFound($"Book with Id = {id} not found");
                    }

                     await bookRepository.DeleteBook(id);
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
}

