using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public int Point { get; set; }
        public string BorrowStatus { get; set; }
    }
}