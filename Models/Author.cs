namespace BookReviewApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }

        // Aly -> Better to have this decorated as virtual and add CountryId property
        public Country Country { get; set; }

        // Aly -> Better to be decorated as virtual
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
