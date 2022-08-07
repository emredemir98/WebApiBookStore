using System.Collections.Generic;
using System.Linq;
using WebApiBookStore.Common;
using WebApiBookStore.DBOperations;

namespace WebApiBookStore.BookOperations.GetBooksById
{
    public class GetBooksByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GetBooksByIdViewModel Handle(int Id)
        {
            var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
            GetBooksByIdViewModel vm = new GetBooksByIdViewModel();

            vm.Title = book.Title;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            vm.PageCount = book.PageCount;

            return vm;

        }
        public class GetBooksByIdViewModel
        {

            public string Title { get; set; }

            public int PageCount { get; set; }

            public string PublishDate { get; set; }

            public string Genre { get; set; }

        }
    }
}
