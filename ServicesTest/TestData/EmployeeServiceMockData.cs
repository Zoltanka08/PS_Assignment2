using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace ServicesTest.TestData
{
    public static class EmployeeServiceMockData
    {
        public static User user = new User()
        {
            Id = 1,
            Username = "MockUser",
            Password = "aDas_123",
            Firstname = "MockFirstname",
            Lastname = "MockLastname",
            Mobile = "0925822950",
            Mail = "mock_mock@mock.com",
            Role = "MockRole"
        };

        public static User userPasswordWithNoUppercase = new User()
        {
            Id = 1,
            Username = "MockUser",
            Password = "adas_123",
            Firstname = "MockFirstname",
            Lastname = "MockLastname",
            Mobile = "0925822950",
            Mail = "mock_mock@mock.com",
            Role = "MockRole"
        };

        public static User userPasswordWithNoNumbers = new User()
        {
            Id = 1,
            Username = "MockUser",
            Password = "Asc_dfa",
            Firstname = "MockFirstname",
            Lastname = "MockLastname",
            Mobile = "0925822950",
            Mail = "mock_mock@mock.com",
            Role = "MockRole"
        };

        public static User userPasswordWithNoSpecialCharacters = new User()
        {
            Id = 1,
            Username = "MockUser",
            Password = "Asd123",
            Firstname = "MockFirstname",
            Lastname = "MockLastname",
            Mobile = "0925822950",
            Mail = "mock_mock@mock.com",
            Role = "MockRole"
        };

        public static IEnumerable<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "MockUser1",
                Password = "MockPassword1",
                Firstname = "MockFirstname1",
                Lastname = "MockLastname1",
                Mobile = "0925822950",
                Mail = "mock_mock@mock.com",
                Role = "MockRole"
            },
            new User()
            {
                Id = 2,
                Username = "MockUser2",
                Password = "MockPassword2",
                Firstname = "MockFirstname2",
                Lastname = "MockLastname2",
                Mobile = "0925822950",
                Mail = "mock_mock@mock.com",
                Role = "MockRole"
            },
            new User()
            {
                Id = 3,
                Username = "MockUser3",
                Password = "MockPassword3",
                Firstname = "MockFirstname3",
                Lastname = "MockLastname3",
                Mobile = "0925822950",
                Mail = "mock_mock@mock.com",
                Role = "MockRole"
            }
        };
    }
}
