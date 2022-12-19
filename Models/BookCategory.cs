namespace BookReviewApp.Models
{
	public class BookCategory
	{
		public int BookId { get; set; }
		public int CategoryId { get; set; }
		public virtual Book Book { get; set; } = new();
		public virtual Category Category { get; set; } = new();
	}
}
