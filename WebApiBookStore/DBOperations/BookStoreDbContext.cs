using Microsoft.EntityFrameworkCore;

namespace WebApiBookStore.DBOperations
{
    public class BookStoreDbContext :DbContext // Db operasyonları için kullanılacak olan DB Context'i yaratılması
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {}
        public DbSet<Book> Books { get; set; }
    }
}
