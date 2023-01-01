namespace BookReviewApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public DateTime ReleaseDateUTC { get; set; }
        public virtual Author Author { get; set; } = new();
        public virtual ICollection<Review>? Reviews { get; set; }
        //public virtual ICollection<BookCategory>? BookCategories { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }

    }
}