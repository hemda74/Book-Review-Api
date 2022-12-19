namespace BookReviewApp.Models
{
	public class Reviewer
	{
		public int ReviewerId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public virtual ICollection<Review>? Reviews { get; set; }
	}
}
