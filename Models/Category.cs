namespace BookReviewApp.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
       
		public string Name { get; set; } = string.Empty;
        public virtual ICollection<Book>? Books { get; set; }

    }
}
