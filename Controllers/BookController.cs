using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookReviewApp.Controllers
{
    // book conttroler
    // you will find old bookcontroller in anthor controller commented
    // 1- add asyncrouns programming 
    // 2- try to fix query db twice
    // plaese look at  --> changes in BookExists and GetBookRating method 
    // i can't find any value of DTO so i stoped working with it  
    namespace BookReviewApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BookController : ControllerBase
        {
            private readonly IBookRepository bookRepository;
            private readonly IReviewRepository reviewRepository;

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
            [HttpGet("{bookname:string}")]
            public async Task<ActionResult<Book>> GetAuthorByName(string name)
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
            // Ahmed  is that method right or how should i code it ?
            [HttpGet("{bookid:int}")]
            public async Task<ActionResult<Book>> BookExists(int id)
            {
                try
                {
                    var result = await bookRepository.BookExists(id);

                    if (result == null) return NotFound();

                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }
            // Ahmed --> please look at this method check wether it correct or not and if not please build it correctly
            [HttpGet("{bookid:int}/rating")]
            public async Task<ActionResult<decimal>> GetBookRating(int bookId)
            {
                var res = await bookRepository.BookExists(bookId);
                if (res == null)
                {
                    return NotFound();
                }
                else
                {
                    var book= bookRepository.GetBookRating(bookId);
                    if(!ModelState.IsValid)
                        return BadRequest(ModelState);

                     return Ok(book);
                    
                }  
            }
            // please look at this method too
            [HttpPost("createbook")]
            public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
            {
                try
                {

                    if (book == null)
                        return BadRequest();
                    // Add custom model validation error method
                    var authid = bookRepository.BookExists(book.BookId);
                    if (authid != null)
                    {
                        ModelState.AddModelError("BookId", "Book Id already in use ");
                        return BadRequest(ModelState);
                    }
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
            public async Task<ActionResult<Book>> UpdateAuthor(int id, Book book)
            {
                try
                {
                    // check for id first
                    if (id != book.BookId)
                        return BadRequest("Book ID mismatch");

                    var bookUpdate = await bookRepository.GetBookById(id);

                    if (bookUpdate == null)
                        return NotFound($"Book with Id = {id} not found");

                    return await bookRepository.UpdateBook(book);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating data");
                }
            }
            // handel delete method
            [HttpDelete("{bookid:int}/delete")]
            public async Task<ActionResult<Book>> DeleteBook(int id)
            {
                try
                {
                    var bookToDelete = await bookRepository.BookExists(id);

                    if (bookToDelete == null)
                    {
                        return NotFound($"Book with Id = {id} not found");
                    }

                    return await bookRepository.DeleteBook(id);
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

