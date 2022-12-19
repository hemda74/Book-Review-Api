namespace BookReviewApp.Models
{
    public class Author :Country
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }

        // Aly -> Better to have this decorated as virtual and add CountryId property
        // Ahmed -> Handled (override prop countryId in this class )
        public override int CountryId { get => base.CountryId; set => base.CountryId = value; }

        // Aly -> Better to be decorated as virtual
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
