using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApiBookStore.BookOperations.CreateBook;
using WebApiBookStore.BookOperations.GetBooks;
using WebApiBookStore.BookOperations.GetBooksById;
using WebApiBookStore.BookOperations.UpdateBook;
using WebApiBookStore.DBOperations;
using static WebApiBookStore.BookOperations.CreateBook.CreateBookCommand;
using static WebApiBookStore.BookOperations.UpdateBook.UpdateBookQuery;

namespace WebApiBookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")] // Gelen requesti hangi resorce'un karşılayacağı bilgisi
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        //private static List<Book> BookList = new List<Book>()
        //    {
        //        new Book
        //        {
        //            Id = 1,
        //            Title ="Lean Startup",
        //            GenreId = 1, //Personal Growth
        //            PageCount = 200,
        //            PublishDate = new DateTime(2001,06,12)
        //        },
        //        new Book
        //        {
        //            Id = 2,
        //            Title ="Herland",
        //            GenreId = 2, //Science Fiction
        //            PageCount = 250,
        //            PublishDate = new DateTime(2010,05,23)
        //        },
        //         new Book
        //        {
        //            Id = 3,
        //            Title ="Herland",
        //            GenreId = 2, //Science Fiction
        //            PageCount = 540,
        //            PublishDate = new DateTime(2001,12,21)
        //        }

        //    };
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBooksByIdQuery query = new GetBooksByIdQuery(_context);
            var result = query.Handle(id);
            return Ok(result);
        }
        //[HttpGet]
        //public Book Get([FromQuery]string id)// Swagger üzerinde /Books şeklinde gözükeceği için tüm listeyi getiren get ile karıştırılacağı için tecih edilmez
        //{
        //    var book = BookList.Where(book => book.Id ==Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}
        //Post 
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook) // hata döndürme işlemi yapacağımız için IActionResult kullandık
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {

                command.Model = newBook;
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //Put

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook)
        {
            try
            {
                UpdateBookQuery query = new UpdateBookQuery(_context);
                query.Model = updatedBook;
                query.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
