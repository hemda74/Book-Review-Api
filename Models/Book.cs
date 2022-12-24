namespace BookReviewApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        // Aly -> Always store date time as UTC, to handle different users time zones, consider you are always dealing with users from all over the world
        public DateTime ReleaseDateUTC { get; set; }

        public virtual Author Author { get; set; } = new();
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<BookCategory>? BookCategories { get; set; }
    }
}