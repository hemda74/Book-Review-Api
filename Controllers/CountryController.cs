using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        public CountryController(ICountryRepository countryRepository) { 
        this.countryRepository = countryRepository;
        }
        
        // handel get all categories
        [HttpGet]
        public async Task<ActionResult> GetCountries()
        {
            try
            {
                return Ok(await countryRepository.GetCountries());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        // handel get country by id method 
        [HttpGet("{countryid:int}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            try
            {
                var result = await countryRepository.GetCountryById(id);

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
        [HttpGet("{countryname}")]
        public async Task<ActionResult<Country>> GetCountryByName(string name)
        {
            try
            {
                var result = await countryRepository.GetCountryByName(name);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("addcountry")]
        public async Task<ActionResult<Country>> AddCountry([FromBody] Country country)
        {
            try
            {

                if (country == null)
                    return BadRequest();
                // Add custom model validation error method
                var catid = countryRepository.CountryExists(country.CountryId);
                if (catid != null)
                {
                    ModelState.AddModelError("CountryId", "Countryry Id already in use ");
                    return BadRequest(ModelState);
                }
                var createdCan = await countryRepository.AddCountry(country);

                return CreatedAtAction(nameof(GetCountryById),
                    new { id = createdCan.CountryId }, createdCan);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Country record");
            }
        }
        [HttpGet("/authors/{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAnAuthor(int authorId)
        {
            var country = countryRepository.GetCountryIdByAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }
        [HttpGet("{countryid:int}")]
        public async Task<ActionResult<Country>> CountryExists(int id)
        {
            try
            {
                var result = await countryRepository.CountryExists(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // handle update method
        [HttpPut("{countryid:int}/updatecountry")]
        public async Task<ActionResult<Country>> UpdateCountry(int id, Country country)
        {
            try
            {
                // check for id first
                if (id != country.CountryId)
                    return BadRequest("Country Id mismatch");
                // please check this line like this or this >>>>>>>>>>>>>var authorToUpdate = await authorRepository.GetAuthorById(id);
                var catToUpdate = await countryRepository.CountryExists(id);

                if (catToUpdate == null)
                    return NotFound($"Category with Id = {id} not found");

                return await countryRepository.UpdateCountry(country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        // handel delete method
        [HttpDelete("{country:int}/deletecountry")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            try
            {
                
                var catDelete = await countryRepository.CountryExists(id);

                if (catDelete == null)
                {
                    return NotFound($"Country with Id = {id} not found");
                }

                return await countryRepository.DeleteCountry(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

    }
}
