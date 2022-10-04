using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class StudentClassVM
    {
        public List<Student> Students { get; set; }
        public List<StudentClass> Classes { get; set; }
        public int BookID { get; set; }
        public int BorrowID { get; set; }
    }
}