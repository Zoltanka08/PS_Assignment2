using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace ServicesTest.TestData
{
    public static class BookServiceMockData
    {
        public static Book book = new Book() 
        {
            Id = 1,
            Title = "MockTitle",
            Genre = "MockGenre",
            Description = "MockDescription",
            Author = "MockAuthor",
            Pages = 0,
            Published = "MockPublished",
            Price = 0,
            Quantity = 0
        };

        public static IEnumerable<Book> books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "MockTitle1",
                Genre = "MockGenre1",
                Description = "MockDescription1",
                Author = "MockAuthor1",
                Pages = 0,
                Published = "MockPublished1",
                Price = 0,
                Quantity = 0
            },
            new Book()
            {
                Id = 2,
                Title = "MockTitle2",
                Genre = "MockGenre2",
                Description = "MockDescription2",
                Author = "MockAuthor2",
                Pages = 0,
                Published = "MockPublished2",
                Price = 0,
                Quantity = 0
            },
            new Book() 
            {
                Id = 3,
                Title = "MockTitle3",
                Genre = "MockGenre3",
                Description = "MockDescription3",
                Author = "MockAuthor3",
                Pages = 0,
                Published = "MockPublished3",
                Price = 0,
                Quantity = 0
            }
        };
    }
}
