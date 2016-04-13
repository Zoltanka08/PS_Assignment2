using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using XMLDatabase.Models;
using Bookstore.Models;
using ElectroShopMobile.CustomAttributes;
using Services.CustomExceptions;
using Bookstore.FactoryDP.AbstractProductInterface;
using Bookstore.FactoryDP.Assembler;

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

        [UserAuthorize(Roles = "admin,employee")]
        public ActionResult Index(string searchTerm = null, bool hasReport = false)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Book, BookViewModel>(); });
            IMapper mapper = config.CreateMapper();

            IEnumerable<Book> books = bookService.GetAll().
                Where(b => searchTerm == null 
                    || b.Genre.StartsWith(searchTerm)
                    || b.Title.StartsWith(searchTerm)
                    || b.Author.StartsWith(searchTerm)
                    || b.Description.StartsWith(searchTerm));
            IEnumerable<BookViewModel> bookModels = mapper.Map<IEnumerable<Book>,IEnumerable<BookViewModel>>(books);
            if (hasReport)
                ViewBag.Success = true;
            return View(bookModels);
        }

        [HttpGet]
        [UserAuthorize(Roles = "employee")]
        public ActionResult SellBook(int id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Book, BookViewModel>(); });
            IMapper mapper = config.CreateMapper();

            Book book = bookService.GetById(id);
            BookViewModel bookModel = mapper.Map<Book, BookViewModel>(book);

            return View(bookModel);
        }

        [HttpPost]
        [UserAuthorize(Roles = "employee")]
        public ActionResult SellBook(BookViewModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<BookViewModel, Book>(); });
            IMapper mapper = config.CreateMapper();

            Book book = mapper.Map<BookViewModel, Book>(model);
            book.Quantity = book.Quantity - model.Number;
            if(book.Quantity < 0)
            {
                ModelState.AddModelError("CustomError", "Sell has been failed! Not enought books on stock!");
                return View(model);
            }

            try
            {
                bookService.Update(book);
            }
            catch(DatabaseException)
            {
                ModelState.AddModelError("CustomError", "Sell has been failed!");
                return View(model);
            }

            return RedirectToAction("Index", "Books", null); 
        }

        [UserAuthorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Book, BookViewModel>(); });
            IMapper mapper = config.CreateMapper();

            Book book = bookService.GetById(id);
            BookViewModel bookModel = mapper.Map<Book, BookViewModel>(book);
            return View(bookModel);
        }

        [HttpPost]
        [UserAuthorize(Roles = "admin")]
        public ActionResult Edit(BookViewModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<BookViewModel, Book>(); });
            IMapper mapper = config.CreateMapper();

            Book book = mapper.Map<BookViewModel, Book>(model);
            try
            {
                bookService.Update(book);
            }
            catch(DatabaseException)
            {
                ModelState.AddModelError("", "Edit has been failed!");
                return View(model);
            }

            return RedirectToAction("Index", "Books", null);
        }

        [HttpGet]
        [UserAuthorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                bookService.Delete(id);
            }
            catch(DatabaseException)
            {
                Response.Write(@"<SCRIPT LANGUAGE=""JavaScript"">alert('" + "Delete fas been failed!" + "')</SCRIPT>");
            }
            return RedirectToAction("Index","Books",null);
        }

        [UserAuthorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorize(Roles = "admin")]
        public ActionResult Create(BookViewModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<BookViewModel, Book>(); });
            IMapper mapper = config.CreateMapper();

            Book book = mapper.Map<BookViewModel, Book>(model);
            try
            {
                bookService.Insert(book);
            }
            catch (DatabaseException)
            {
                ModelState.AddModelError("", "Insert has been failed!");
                return View(model);
            }

            return RedirectToAction("Index", "Books", null);
        }

        public ActionResult Report()
        {
            List<SelectListItem> items = GetSelectListForReportType();
            ViewBag.ReportType = items;
            return View();
        }

        [HttpPost]
        public ActionResult Report(string ReportType)
        {
            string reportTypeName = GetReportTypeById(ReportType);

            ReportAssembler reportAssembler = new ReportAssembler(reportTypeName);
            IReport report = reportAssembler.AssembleReport();
            report.CreateReport(bookService);

            return RedirectToAction("Index", "Books", new { hasReport = true });
        }
        
        private List<SelectListItem> GetSelectListForReportType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Selected = true,
                Text = "txt",
                Value = "1"
            });
            items.Add(new SelectListItem()
            {
                Selected = true,
                Text = "xml",
                Value = "2"
            });
            return items;
        }

        private string GetReportTypeById(string id)
        {
            string type = GetSelectListForReportType().First(r => r.Value.Equals(id)).Text;
            return type;
        }

    }
}
