using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Shared
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string WelshLanguageProficiency { get; set; }

    }
}
