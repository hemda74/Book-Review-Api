using Microsoft.EntityFrameworkCore;
using BookReviewApp.Models;

namespace BookReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>()
                    .HasKey(pc => new { pc.BookId, pc.CategoryId });
            modelBuilder.Entity<BookCategory>()
                    .HasOne(p => p.Book)
                    .WithMany(pc => pc.BookCategories)
                    .HasForeignKey(p => p.BookId);
            modelBuilder.Entity<BookCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.BookCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<BookAuthor>()
                    .HasKey(po => new { po.BookId, po.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                    .HasOne(p => p.Book)
                    .WithMany(pc => pc.BookAuthor)
                    .HasForeignKey(p => p.BookId);
            modelBuilder.Entity<BookAuthor>()
                    .HasOne(p => p.Author)
                    .WithMany(pc => pc.BookAuthors)
                    .HasForeignKey(c => c.AuthorId);
        }
    }
}
