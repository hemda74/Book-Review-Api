namespace BookReviewApp.Models
{
//<<<<<<< Updated upstream
	public class Country
	{
		// Aly -> Primary key can't be virtual, we only decorate foreign entities as virtual
		public int CountryId { get; set; }

		public string Name { get; set; } = string.Empty;

		// Aly -> This one should be decorated as virtual, because it's foreign entity
		// Aly -> Also it's nullable because a country may have no authors
		public virtual ICollection<Author>? Authors { get; set; }
	}
//=======
//    public class Country
//    {
//        public  int Id { get; set; }

//        public virtual string CountryName { get; set; }
//        public ICollection<Author> Authors { get; set; }
//    }
//>>>>>>> Stashed changes
}
