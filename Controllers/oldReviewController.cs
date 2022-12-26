//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using BookReviewApp.Dto;
//using BookReviewApp.Interfaces;
//using BookReviewApp.Models;

//namespace BookReviewApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class oldReviewController : Controller
//    {
//        private readonly IReviewRepository _reviewRepository;
//        private readonly IMapper _mapper;
//        private readonly IReviewerRepository _reviewerRepository;
//        private readonly IBookRepository _bookRepository;

//        public oldReviewController(IReviewRepository reviewRepository, 
//            IMapper mapper,
//            IBookRepository bookRepository,
//            IReviewerRepository reviewerRepository)
//        {
//            _reviewRepository = reviewRepository;
//            _mapper = mapper;
//            _reviewerRepository = reviewerRepository;
//            _bookRepository = bookRepository;
//        }

//        [HttpGet]
//        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
//        public IActionResult GetReviews()
//        {
//            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(reviews);
//        }

//        [HttpGet("{reviewId}")]
//        [ProducesResponseType(200, Type = typeof(Review))]
//        [ProducesResponseType(400)]
//        public IActionResult GetBook(int reviewId)
//        {
//            if (!_reviewRepository.ReviewExists(reviewId))
//                return NotFound();

//            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(review);
//        }

//        [HttpGet("book/{bookId}")]
//        [ProducesResponseType(200, Type = typeof(Review))]
//        [ProducesResponseType(400)]
//        public IActionResult GetReviewsForAPokemon(int bookId)
//        {
//            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfABook(bookId));

//            if (!ModelState.IsValid)
//                return BadRequest();

//            return Ok(reviews);
//        }

//        [HttpPost]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(400)]
//        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery]int bookId, [FromBody] ReviewDto reviewCreate)
//        {
//            if (reviewCreate == null)
//                return BadRequest(ModelState);

//            var reviews = _reviewRepository.GetReviews()
//                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
//                .FirstOrDefault();

//            if (reviews != null)
//            {
//                ModelState.AddModelError("", "Review already exists");
//                return StatusCode(422, ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var reviewMap = _mapper.Map<Review>(reviewCreate);

//            reviewMap.Book = _bookRepository.GetBook(bookId);
//            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            

//            if (!_reviewRepository.CreateReview(reviewMap))
//            {
//                ModelState.AddModelError("", "Something went wrong while saving");
//                return StatusCode(500, ModelState);
//            }

//            return Ok("Successfully created");
//        }

//        [HttpPut("{reviewId}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
//        {
//            if (updatedReview == null)
//                return BadRequest(ModelState);

//            if (reviewId != updatedReview.Id)
//                return BadRequest(ModelState);

//            if (!_reviewRepository.ReviewExists(reviewId))
//                return NotFound();

//            if (!ModelState.IsValid)
//                return BadRequest();

//            var reviewMap = _mapper.Map<Review>(updatedReview);

//            if (!_reviewRepository.UpdateReview(reviewMap))
//            {
//                ModelState.AddModelError("", "Something went wrong updating review");
//                return StatusCode(500, ModelState);
//            }

//            return NoContent();
//        }

//        [HttpDelete("{reviewId}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        public IActionResult DeleteReview(int reviewId)
//        {
//            if (!_reviewRepository.ReviewExists(reviewId))
//            {
//                return NotFound();
//            }

//            var reviewToDelete = _reviewRepository.GetReview(reviewId);

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if (!_reviewRepository.DeleteReview(reviewToDelete))
//            {
//                ModelState.AddModelError("", "Something went wrong deleting author");
//            }

//            return NoContent();
//        }
        
//        // Added missing delete range of reviews by a reviewer **>CK
//        [HttpDelete("/DeleteReviewsByReviewer/{reviewerId}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//    public IActionResult DeleteReviewsByReviewer(int reviewerId)
//    {
//        if (!_reviewerRepository.ReviewerExists(reviewerId))
//            return NotFound();

//        var reviewsToDelete = _reviewerRepository.GetReviewsByReviewer(reviewerId).ToList();
//        if (!ModelState.IsValid)
//            return BadRequest();

//        if (!_reviewRepository.DeleteReviews(reviewsToDelete))
//        {
//            ModelState.AddModelError("", "error deleting reviews");
//            return StatusCode(500, ModelState);
//        }
//        return NoContent();
//    }

//    }
//}
