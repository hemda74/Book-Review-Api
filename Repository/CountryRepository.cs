using AutoMapper;
using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;


        public CountryRepository(DataContext context)
        {
            _context = context;

        }
        // method to add country 
        public async Task<Country> AddCountry(Country country)
        {
            var result = await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // check if country exsists or not 
        public async Task<bool> CountryExists(int countryId)
        {
            // fail first 
            if (countryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countryId));
            }
            return await _context.Countries.AnyAsync(o => o.CountryId == countryId);
        }
        //Delete country method 
        public async Task DeleteCountry(int countryId)
        {
            var result = await _context.Countries
                    .FirstOrDefaultAsync(e => e.CountryId == countryId);
            if (result != null)
            {
                _context.Countries.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        // return all countries
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
        // method to return country by id 
        public async Task<Country?> GetCountryById(int countryId)
        {
            // validate the given id first 
            if (countryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countryId));
            }

            return await _context.Countries
                .FirstOrDefaultAsync(e => e.CountryId == countryId);
        }
        // return country by name 
        public async Task<Country?> GetCountryByName(string name)
        {
            if (name == null )
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            return await _context.Countries
                .FirstOrDefaultAsync(e => e.CountryName == name);
        }

        public async Task<Country> UpdateCountry(Country country)
        { 
            // check the id 
            if (country.CountryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(country));
            }
            var result = await _context.Countries
            .FirstOrDefaultAsync(e => e.CountryId == country.CountryId);
            // check the return vaules from data base 
            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(country));
            }
            result.Authors = country.Authors;
            result.CountryName = country.CountryName;
            await _context.SaveChangesAsync();
            return result;   
        }
    }
}
