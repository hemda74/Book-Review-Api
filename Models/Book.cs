using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection <BookAuthor> BookAuthor { get; set; }
        public ICollection <BookCategory> BookCategories { get; set; }
    }
}
