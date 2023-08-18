using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentEnrolment.Shared.Data
{
    public class EnrolmentContext : DbContext
    {
        public EnrolmentContext(DbContextOptions<EnrolmentContext> options) : base(options) 
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
