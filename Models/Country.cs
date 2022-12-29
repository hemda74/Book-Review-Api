namespace BookReviewApp.Models
{
	public class Country
	{
		public int CountryId { get; set; }

		public string Name { get; set; } = string.Empty;

		public virtual ICollection<Author>? Authors { get; set; }
	}
}
