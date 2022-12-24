//using AutoMapper;
//using BookReviewApp.Dto;
//using BookReviewApp.Interfaces;
//using BookReviewApp.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace BookReviewApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class oldAuhtorController : ControllerBase
//    {
//        private readonly IAuthorRepository _authorRepository;
//        private readonly ICountryRepository _countryRepository;
//        private readonly IMapper _mapper;

//        public oldAuhtorController(IAuthorRepository authorRepository,
//            ICountryRepository countryRepository,
//            IMapper mapper)
//        {
//            _authorRepository = authorRepository;
//            _countryRepository = countryRepository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
//        public  async Task<IActionResult> GetAuthors()
//        {
//            var authors = await _authorRepository.GetAuthors();

//            if (!ModelState.IsValid)
//                return StatusCode(422, ModelState);

//            return Ok(authors);
//        }

//        [HttpGet("{authorId}")]
//        [ProducesResponseType(200, Type = typeof(Author))]
//        [ProducesResponseType(400)]
//        public async async <ActionResult> GetAuthorById(int authorId)
//        {
//            try
//            {
//                return Ok(await _authorRepository.GetAuthorById(authorId));
//            }
//            catch (Exception ex) { 
//            }
//            //// Aly -> This method is totally wrong, you are querying database twice, while it's needed
//            //if (!_authorRepository.AuthorExists(authorId))
//            //    return NotFound();

//            //// Aly -> This is not right, mapping parameters are incorrect
//            //// Ahmed -> Handeled change AuthorExsists to Get Author
//            //var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthorById(authorId));

//            //if (!ModelState.IsValid)
//            //    return BadRequest(ModelState);

//            //return Ok(author);
//        }

//        [HttpGet("{authorId}/book")]
//        [ProducesResponseType(200, Type = typeof(Author))]
//        [ProducesResponseType(400)]
//        public IActionResult GetBookByAuthor(int authorId)
//        {
//            // Aly -> This method is totally wrong, you are querying database twice, while it's needed
//            if (!_authorRepository.AuthorExists(authorId))
//            {
//                return NotFound();
//            }

//            // Aly -> This is not right, naming conventions are wrong, mapping parameters are incorrect
//            // Ahmed -> Handled Change AuthorExists To getbookby author
//            var author = _mapper.Map<List<BookDto>>(
//                _authorRepository.GetBookByAuthor(authorId));

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(author);
//        }

//        [HttpPost]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(400)]
//        public IActionResult CreateAuthor([FromQuery] int countryId, [FromBody] AuthorDto authorCreate)
//        {
//            if (authorCreate == null)
//                return BadRequest(ModelState);

//            // Aly -> Should be named author, not authors, it's single result
//            // Ahmed -> Handeld change Authors to Author
//            var author = _authorRepository.GetAuthors()
//                // Aly -> Should be moved down, by creating another method in repository, or create service layer
//                .Where(c => c.LastName.Trim().ToUpper() == authorCreate.LastName.TrimEnd().ToUpper())
//                .FirstOrDefault();
//            // Aly -> Up here, you are querying database twice, which is a very bad practice, let's discuss

//            if (author != null)
//            {
//                ModelState.AddModelError("", "Author already exists");
//                return StatusCode(422, ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var ownerMap = _mapper.Map<Author>(authorCreate);

//            // Aly -> If virtual approach was used here, we would be able to save one call to database, which increases performance
//             //Ahmed -> this line deosnot mean anything after virtual approach used  
//            //ownerMap.CountryId = _countryRepository.GetCountry(countryId);

//            if (!_authorRepository.CreateAuthor(ownerMap))
//            {
//                ModelState.AddModelError("", "Something went wrong while saving");
//                return StatusCode(500, ModelState);
//            }

//            return Ok("Successfully created");
//        }

//        [HttpPut("{authorId}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        public IActionResult UpdateAuthor(
//            // Aly -> Better to specify it's coming from route
//            int authorId,
//            [FromBody] AuthorDto updatedAuthor)
//        {
//            if (updatedAuthor == null)
//                return BadRequest(ModelState);

//            if (authorId != updatedAuthor.AuhtorId)
//                return BadRequest(ModelState);

//            if (!_authorRepository.AuthorExists(authorId))
//                return NotFound();

//            if (!ModelState.IsValid)
//                return BadRequest();

//            var ownerMap = _mapper.Map<Author>(updatedAuthor);

//            if (!_authorRepository.UpdateAuthor(ownerMap))
//            {
//                ModelState.AddModelError("", "Something went wrong updating Author");
//                return StatusCode(500, ModelState);
//            }

//            return NoContent();
//        }

//        [HttpDelete("{authorId}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        public IActionResult DeleteAuthor(int authorId)
//        {
//            if (!_authorRepository.AuthorExists(authorId))
//            {
//                return NotFound();
//            }

//            var authorToDelete = _authorRepository.GetAuthor(authorId);

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if (!_authorRepository.DeleteAuthor(authorToDelete))
//            {
//                ModelState.AddModelError("", "Something went wrong deleting author");
//            }

//            return NoContent();
//        }
//    }
//}
