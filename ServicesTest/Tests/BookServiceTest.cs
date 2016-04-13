using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XMLDatabase.Interfaces;
using Services.Interfaces;
using XMLDatabase.Models;
using Services.CustomExceptions;
using ServicesTest.TestData;
using Ploeh.SemanticComparison;
using Services.Services;
using System.Linq;

namespace ServicesTest.Tests
{
    /// <summary>
    /// Summary description for BookServiceTest
    /// </summary>
    [TestClass]
    public class BookServiceTest
    {
        private Mock<IBookDataAccessor> mockBookDataAccessor = new Mock<IBookDataAccessor>();
        private IBookService mockedBookService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockedBookService = new BookService(mockBookDataAccessor.Object);
        }
        
        [TestMethod]
        public void GetById_CorrectId_ExpectedCorrectBook()
        {
            //Arrange
            int correctId = 1;
            Book expectedBook = BookServiceMockData.book;
            Likeness<Book, Book> bookLikeness = new Likeness<Book, Book>(expectedBook);
            SetupBookDataAccessorGet(expectedBook);

            //Act
            Book resultBook = mockedBookService.GetById(correctId);

            //Assert
            Assert.AreEqual(bookLikeness,resultBook);
        }

        [TestMethod]
        public void GetAll_CurrentDatabase_ExpectedBookCount()
        {
            //Arrange
            IEnumerable<Book> books = BookServiceMockData.books;
            int expectedCount = 3;
            SetupMockBookDataAccessorGetAll(books);

            //Act
            IEnumerable<Book> resultBooks =  mockedBookService.GetAll();
            int resultCount = resultBooks.Count();

            //Assert
            Assert.AreEqual(expectedCount,resultCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void GetAll_NoBookInDatabase_ExpectedDatabaseException()
        {
            //Arrange
            IEnumerable<Book> books = new List<Book>();
            SetupMockBookDataAccessorGetAll(books);

            //Act
            IEnumerable<Book> resultBooks = mockedBookService.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void GetAll_NullBookData_ExpectedDatabaseException()
        {
            //Arrange
            IEnumerable<Book> books = null;
            SetupMockBookDataAccessorGetAll(books);

            //Act
            IEnumerable<Book> resultBooks = mockedBookService.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Insert_WrongBookData_ExpectedDatabaseException()
        {
            //Arrange
            Book book = BookServiceMockData.book;
            SetupMockBookDataAccessorInsert();

            //Act
            mockedBookService.Insert(book);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Update_WrongBookData_ExpectedDatabaseException()
        {
            //Arrange
            Book book = BookServiceMockData.book;
            SetupMockBookDataAccessorUpdate();

            //Act
            mockedBookService.Update(book);
        }
        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Delete_WrongBookId_ExpectedDatabaseException()
        {
            //Arrange
            int id = -1;
            SetupMockBookDataAccessorDelete();

            //Act
            mockedBookService.Delete(id);
        }

        #region SetupMockMethods

        private void SetupBookDataAccessorGet(Book book)
        {
            mockBookDataAccessor.Setup(b => b.GetById(It.IsAny<int>())).Returns(book);
        }

        private void SetupMockBookDataAccessorGetAll(IEnumerable<Book> books)
        {
            mockBookDataAccessor.Setup(b => b.GetAll()).Returns(books);
        }

        private void SetupMockBookDataAccessorInsert()
        {
            mockBookDataAccessor.Setup(b => b.Insert(It.IsAny<Book>())).Throws(new DatabaseException());
        }

        private void SetupMockBookDataAccessorUpdate()
        {
            mockBookDataAccessor.Setup(b => b.Update(It.IsAny<Book>())).Throws(new DatabaseException());
        }

        private void SetupMockBookDataAccessorDelete()
        {
            mockBookDataAccessor.Setup(b => b.Delete(It.IsAny<int>())).Throws(new DatabaseException());
        }

        #endregion SetupMockMethods
    }
}
