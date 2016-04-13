using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDatabase.Interfaces;
using Moq;
using Services.Interfaces;
using XMLDatabase.Models;
using ServicesTest.TestData;
using Ploeh.SemanticComparison;
using System.Collections.Generic;
using Services.CustomExceptions;
using Services.Services;
using System.Linq;

namespace ServicesTest.Tests
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<IUserDataAccessor> mockUserDataAccessor = new Mock<IUserDataAccessor>();
        private IEmployeeService mockedEmployeeService;

        [TestInitialize]
        public void TestInitializer()
        {
            this.mockedEmployeeService = new EmployeeService(mockUserDataAccessor.Object);
        }

        [TestMethod]
        public void GetUserById_CorrectUsername_ExpectedCorrectUser()
        {
            //Arrange
            string correctUsername = "correct";
            User expectedUser = EmployeeServiceMockData.user;
            Likeness<User, User> userLikeness = new Likeness<User, User>(expectedUser);
            SetupMockEmployeeDataAccessorGetByUsername(expectedUser);

            //Act
            User resultUser = mockedEmployeeService.GetByUsername(correctUsername);

            //Assert
            Assert.AreEqual(userLikeness, resultUser);
        }

        [TestMethod]
        public void GetAll_CurrentDatabase_ExpectedBookCount()
        {
            //Arrange
            int expectedCount = 3;
            IEnumerable<User> users = EmployeeServiceMockData.users;
            SetupMockEmployeeDataAccessorGetAll(users);

            //Act
            int resultCount = mockedEmployeeService.GetAll().Count();

            //Assert
            Assert.AreEqual(expectedCount,resultCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void GetAll_EmptyDatabase_ExpectedDatabaseExcception()
        {
            //Arrange
            SetupMockEmployeeDataAccessorGetAll(new List<User>());

            //Act
            mockedEmployeeService.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Insert_WrongUserData_ExpectedDatabaseException()
        {
            //Arrange
            User wrongUser = EmployeeServiceMockData.userPasswordWithNoUppercase;
            SetupMockEmployeeDataAccessorInsert();

            //Act
            mockedEmployeeService.Insert(wrongUser);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Update_WrongUserData_ExpectedDatabaseException()
        {
            //Arrange
            User wrongUser = EmployeeServiceMockData.userPasswordWithNoUppercase;
            SetupMockEmployeeDataAccessorUpdate();

            //Act
            mockedEmployeeService.Update(wrongUser);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Delete_WrongUserId_ExpectedDatabaseException()
        {
            //Arrange
            int wrongId = -1;
            SetupMockEmployeeDataAccessorDelete();

            //Act
            mockedEmployeeService.Delete(wrongId);
        }

        #region SetupMockService

        private void SetupMockEmployeeDataAccessorGetByUsername(User user)
        {
            mockUserDataAccessor.Setup(u => u.GetUserByUsername(It.IsAny<string>())).Returns(user);
        }

        private void SetupMockEmployeeDataAccessorGetAll(IEnumerable<User> users)
        {
            mockUserDataAccessor.Setup(u => u.GetAll()).Returns(users);
        }

        private void SetupMockEmployeeDataAccessorInsert()
        {
            mockUserDataAccessor.Setup(u => u.Insert(It.IsAny<User>())).Throws(new DatabaseException());
        }

        private void SetupMockEmployeeDataAccessorUpdate()
        {
            mockUserDataAccessor.Setup(u => u.Update(It.IsAny<User>())).Throws(new DatabaseException());
        }

        private void SetupMockEmployeeDataAccessorDelete()
        {
            mockUserDataAccessor.Setup(u => u.Delete(It.IsAny<int>())).Throws(new DatabaseException());
        }

        #endregion

    }
}
