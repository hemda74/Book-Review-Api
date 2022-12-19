namespace BookReviewApp.Models
{
    public class Country
    {
        public virtual int CountryId { get; set; }

        public string Name { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
