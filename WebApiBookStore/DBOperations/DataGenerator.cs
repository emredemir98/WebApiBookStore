

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApiBookStore.DBOperations
{
    public class DataGenerator //Initial Data için bir Data Generator'ın yazılması
    {
        public static void Initialize(IServiceProvider serviceProvider) // uygulama ilk ayağa kalktığında hep çalışacak bir yapı
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any()) //Veri varsa 
                {
                    return;
                }
                context.Books.AddRange( //VEriler eklenir
                new Book
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1, //Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book
                {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2, //Science Fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                 new Book
                 {
                     //Id = 3,
                     Title = "Herland",
                     GenreId = 2, //Science Fiction
                     PageCount = 540,
                     PublishDate = new DateTime(2001, 12, 21)
                 }
               );
                context.SaveChanges(); // db değişiklikler kaydedilir.
            }
        }
    }
}
