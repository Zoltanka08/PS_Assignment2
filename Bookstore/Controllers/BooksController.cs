using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using XMLDatabase.Models;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Book/

        private IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public ActionResult Index()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Book, BookViewModel>(); });
            IMapper mapper = config.CreateMapper();

            IEnumerable<Book> books = bookService.GetAll();
            IEnumerable<BookViewModel> bookModels = mapper.Map<IEnumerable<Book>,IEnumerable<BookViewModel>>(books);

            return View(bookModels);
        }

    }
}
