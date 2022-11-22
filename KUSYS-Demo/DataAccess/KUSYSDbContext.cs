using Microsoft.EntityFrameworkCore;
using Models.Concrete;

namespace DataAccess
{
    public class KUSYSDbContext : DbContext
    {
        
        public KUSYSDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}