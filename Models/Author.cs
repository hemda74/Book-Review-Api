namespace BookReviewApp.Models
{
	public class Author
	{
		public int AuthorId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gym { get; set; } = string.Empty;

		// virtual prop for country prop 
		public virtual ICollection<Country>? Country { get; set; } 
		// virtual prop of Books class 
		public virtual ICollection<Book>? Books { get; set; }
	}
}