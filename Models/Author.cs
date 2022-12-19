namespace BookReviewApp.Models
{
	// Aly -> Models don't inherit from each other except in very special cases, which is not the one here
	public class Author
	{
		// Aly -> Try to stick to one way of naming conventions, either all as "ClassId" or just "Id"
		public int AuthorId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gym { get; set; } = string.Empty;

		// Aly -> This is the foreign key for country model, one country may contain multiple authors, but not the opposite
		public int CountryId { get; set; }

		// Aly -> This is the virtual representation for the foreign country model
		public virtual Country Country { get; set; } = new();

		// Aly -> Better to be decorated as virtual, also nullable because author might have no books
		public virtual ICollection<Book>? Books { get; set; }
	}
}
