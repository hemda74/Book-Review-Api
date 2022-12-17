using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuhtorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public AuhtorController(IAuthorRepository ownerRepository, 
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _authorRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public IActionResult GetAuthors()
        {
            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthor());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();

            var author = _mapper.Map<AuthorDto>(_authorRepository.AuthorExists(authorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(author);
        }

        [HttpGet("{authorId}/book")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        public IActionResult GetBookByAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var author = _mapper.Map<List<BookDto>>(
                _authorRepository.AuthorExists(authorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromQuery] int countryId, [FromBody] AuthorDto authorCreate)
        {
            if (authorCreate == null)
                return BadRequest(ModelState);

            var authors = _authorRepository.GetAuthor()
                .Where(c => c.LastName.Trim().ToUpper() == authorCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (authors != null)
            {
                ModelState.AddModelError("", "Author already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Author>(authorCreate);

            ownerMap.Country = _countryRepository.GetCountry(countryId);

            if (!_authorRepository.CreateAuthor(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto updatedAuthor)
        {
            if (updatedAuthor == null)
                return BadRequest(ModelState);

            if (authorId != updatedAuthor.Id)
                return BadRequest(ModelState);

            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Author>(updatedAuthor);

            if (!_authorRepository.UpdateAuthor(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Author");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var authorToDelete = _authorRepository.GetAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authorRepository.DeleteAuthor(authorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting author");
            }

            return NoContent();
        }
    }
}
