namespace ReadingJournal.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ReadingJournal.DataModels;
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base (options){ }

        public DbSet<Book> Books { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
