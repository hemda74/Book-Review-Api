
using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
    
        Country GetCountry(int id);
        int GetCountryIdByAuthor (int authorId);
        ICollection<Author> GetAuthorsFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}
