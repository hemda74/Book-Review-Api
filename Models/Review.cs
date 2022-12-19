namespace BookReviewApp.Models
{
	public class Review
	{
		public int ReviewId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public int Rating { get; set; }

		public int ReviewerId { get; set; }
		public virtual Reviewer Reviewer { get; set; } = new();

		public int BookId { get; set; }
		public virtual Book Book { get; set; } = new();
	}
}
