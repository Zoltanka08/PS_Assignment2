using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDatabase.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Published { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
