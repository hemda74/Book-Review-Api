namespace BookReviewApp.Models
{
	public class Author
	{
		public int AuthorId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gym { get; set; } = string.Empty;

		public int CountryId { get; set; }
		public virtual Country Country { get; set; } = new();

		public virtual ICollection<Book>? Books { get; set; }
	}
}