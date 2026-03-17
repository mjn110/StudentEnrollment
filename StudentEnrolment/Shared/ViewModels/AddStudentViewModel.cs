using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Shared.ViewModels
{
    public class AddStudentViewModel
    {
        [DisplayName("Student Name")]
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [DisplayName("Course Name")]
        [Required]
        public string CourseName { get; set; }

        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DisplayName("End Date")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [DisplayName("Welsh Language Proficiency")]
        [Required]
        public string WelshLanguageProficiency { get; set; }
    }
}
