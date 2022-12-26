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
        

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            
        }
        // check if country exsists or not 
        public  async Task<IEnumerable<Country>> CountryExists(int countryId)
        {
            IQueryable<Country> query = _context.Countries;

            if (countryId != null)
            {
                query = query.Where(e => e.CountryId == countryId);
            }

            return await query.ToListAsync();
        }
        // add country method 
        public async Task<Country> AddCountry(Country country)
        {
            var result = await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        // delete country method 
        public async Task<Country> DeleteCountry(int countryId)
        {
            var result = await _context.Countries
                .FirstOrDefaultAsync(e => e.CountryId == countryId);
            if (result != null)
            {
                _context.Countries.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }
        // Ahmed ----> please check this method 
        public ICollection<Author> GetAuthorsFromACountry(int countryId)
        {
            return _context.Authors.Where(c => c.CountryId == countryId).ToList();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
        // get country by countryid 
        public async Task<Country> GetCountryById(int countryId)
        {
            return await _context.Countries
               .FirstOrDefaultAsync(e => e.CountryId == countryId);
        }

        public Country GetCountryIdByAuthor(int authorId)
        {
            return _context.Authors.Where(o => o.AuthorId == authorId).Select(c => c.Country).FirstOrDefault();

        }
        // handle edit method 
        public async Task<Country> UpdateCountry(Country country)
        {
            var result = await _context.Countries
           .FirstOrDefaultAsync(e => e.CountryId == country.CountryId);

            if (result != null)
            {
                result.Name = country.Name;
                result.Authors = country.Authors;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
        // get country by name 
        public async Task<Country> GetCountryByName(string name)
        {
            return await _context.Countries
             .FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}
//        public bool CountryExists(int id)
//        {
//            return _context.Countries.Any(c => c.CountryId == id);
//        }

//        public bool CreateCountry(Country country)
//        {
//            _context.Add(country);
//            return Save();
//        }

//        public bool DeleteCountry(Country country)
//        {
//            _context.Remove(country);
//            return Save();
//        }

//        public ICollection<Author> GetAuthorsFromACountry(int countryId2)
//        {
//            return _context.Authors.Where(c => c.CountryId == countryId2).ToList();
//        }

//        public ICollection<Country> GetCountries()
//        {
//            return _context.Countries.ToList();
//        }

//        public Country GetCountry(int id)
//        {
//            return _context.Countries.Where(c => c.CountryId == id).FirstOrDefault();
//        }

//        public int  GetCountryIdByAuthor(int cId)
//        {
//            return _context.Authors.Where(o => o.Id == cId).Select(c => c.CountryId).FirstOrDefault();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public bool UpdateCountry(Country country)
//        {
//            _context.Update(country);
//            return Save();
//        }
//    }
//}
