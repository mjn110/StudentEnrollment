using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Shared.ViewModels
{
    public class UpdateStudentViewModel
    {
        [DisplayName("Student Name")]
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [DisplayName("Welsh Language Proficiency")]
        [Required]
        public string WelshLanguageProficiency { get; set; }
    }
}
