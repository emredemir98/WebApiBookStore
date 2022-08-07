using System;
using System.Linq;
using WebApiBookStore.DBOperations;

namespace WebApiBookStore.BookOperations.UpdateBook
{
    public class UpdateBookQuery
    {
        public readonly BookStoreDbContext _dbContext;

        public UpdateBookViewModel Model { get; set; }

        public UpdateBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;

            _dbContext.SaveChanges();

        }
        public class UpdateBookViewModel
        {
            public string Title { get; set; }

            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }

            public int GenreId { get; set; }
        }
    }
}
