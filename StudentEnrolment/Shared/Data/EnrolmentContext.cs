using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentEnrolment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentEnrolment.Shared.Data
{
    public class EnrolmentContext : IdentityDbContext<ApplicationUser>
    {
        public EnrolmentContext(DbContextOptions<EnrolmentContext> options) : base(options) 
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
