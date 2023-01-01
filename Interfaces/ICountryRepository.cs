
using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country?> GetCountryById (int countryId);
        Task<Country?> GetCountryByName(string name);
        Task<bool> CountryExists(int countryId);
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task DeleteCountry(int countryId);
    }
}
