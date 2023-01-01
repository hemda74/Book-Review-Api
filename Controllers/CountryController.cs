using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        
        [HttpGet("{countryid:int}/Exists")]
        public async Task<ActionResult<Country>> CountryExists(int id)
        {
            try
            {
                var result = await countryRepository.CountryExists(id);

                if (!result) return NotFound();

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
        public async Task<ActionResult<Country>> UpdateCountry(Country country)
        {
            try
            {
                if (country==null)
                    return BadRequest("Country Id mismatch");
                
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
                if (id <= 0)
                {
                    return BadRequest($"Country Id not found");
                }

                 await countryRepository.DeleteCountry(id);
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
