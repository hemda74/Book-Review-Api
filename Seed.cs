//using BookReviewApp.Data;
//using BookReviewApp.Models;

//namespace BookReviewApp
//{
//    public class Seed
//    {
//        private readonly DataContext dataContext;
//        public Seed(DataContext context)
//        {
//            this.dataContext = context;
//        }
//        public void SeedDataContext()
//        {
//            if (!dataContext.BookAuthors.Any())
//            {
//                var bookAuthors = new List<BookAuthor>()
//                {
//                    new BookAuthor()
//                    {
//                        Book = new Book()
//                        {
//                            Name = "Pikachu",
//                            ReleaseDate = new DateTime(1903,1,1),
//                            BookCategories = new List<BookCategory>()
//                            {
//                                new BookCategory { Category = new Category() { Name = "Electric"}}
//                            },
//                            Reviews = new List<Review>()
//                            {
//                                new Review { Title="Pikachu",Text = "Pickahu is the best Book, because it is electric", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
//                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
//                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
//                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
//                            }
//                        },
//                        Author = new Author()
//                        {
//                            FirstName = "Jack",
//                            LastName = "London",
//                            Gym = "Brocks Gym",
//                            Country = new Country()
//                            {
//                                Name = "Kanto"
//                            }
//                        }
//                    },
//                    new BookAuthor()
//                    {
//                        Book = new Book()
//                        {
//                            Name = "Pikachu",
//                            ReleaseDate = new DateTime(1903,1,1),
//                            BookCategories = new List<BookCategory>()
//                            {
//                                new BookCategory { Category = new Category() { Name = "Electric"}}
//                            },
//                            Reviews = new List<Review>()
//                            {
//                                new Review { Title="Pikachu",Text = "Pickahu is the best Book, because it is electric", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
//                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
//                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
//                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
//                            }
//                        },
//                        Author = new Author()
//                        {
//                            FirstName = "Jack",
//                            LastName = "London",
//                            Gym = "Brocks Gym",
//                            Country = new Country()
//                            {
//                                Name = "Kanto"
//                            }
//                        }
//                    },
//                        new BookAuthor()
//                    {
//                        Book = new Book()
//                        {
//                            Name = "Pikachu",
//                            ReleaseDate = new DateTime(1903,1,1),
//                            BookCategories = new List<BookCategory>()
//                            {
//                                new BookCategory { Category = new Category() { Name = "Electric"}}
//                            },
//                            Reviews = new List<Review>()
//                            {
//                                new Review { Title="Pikachu",Text = "Pickahu is the best Book, because it is electric", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
//                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
//                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
//                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
//                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
//                            }
//                        },
//                        Author = new Author()
//                        {
//                            FirstName = "Jack",
//                            LastName = "London",
//                            Gym = "Brocks Gym",
//                            Country = new Country()
//                            {
//                                Name = "Kanto"
//                            }
//                        }
//                    }
//                };
//                dataContext.BookAuthors.AddRange(bookAuthors);
//                dataContext.SaveChanges();
//            }
//        }
//    }
//}
