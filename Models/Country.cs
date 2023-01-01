namespace BookReviewApp.Models
{
	public class Country
	{
		public int CountryId { get; set; }

		public string CountryName { get; set; } = string.Empty;

		public virtual ICollection<Author>? Authors { get; set; }
	}
}
