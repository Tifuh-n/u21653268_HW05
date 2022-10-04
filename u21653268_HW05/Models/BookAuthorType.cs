using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class BookAuthorType
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Type> Types { get; set; }

    }
}