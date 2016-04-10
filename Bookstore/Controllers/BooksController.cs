﻿using Services.Interfaces;
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
        public ActionResult Index(string searchTerm = null)
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
                ModelState.AddModelError("", "Delete has been failed!");
                MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Book, BookViewModel>(); });
                IMapper mapper = config.CreateMapper();

                Book book = bookService.GetById(id);
                BookViewModel bookModel = mapper.Map<Book, BookViewModel>(book);
                return View(bookModel);
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
    }
}
