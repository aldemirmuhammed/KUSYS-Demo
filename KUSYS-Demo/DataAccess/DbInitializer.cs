using Models.Concrete;

namespace DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(KUSYSDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any course.
            if (!context.Courses.Any())
            {
                // Create course for default
                var courses = new Course[]
                     {
                        new Course{Code="1050",Name="Chemistry"},
                        new Course{Code="4022",Name="Microeconomics"},
                        new Course{Code="4041",Name="Macroeconomics"},
                        new Course{Code="1045",Name="Calculus"},
                        new Course{Code="3141",Name="Trigonometry"},
                        new Course{Code="2021",Name="Composition"},
                        new Course{Code="2042",Name="Literature"}
                     };

                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }
                context.SaveChanges();
            }

            // Look for any Admins.
            if (!context.Admins.Any())
            {
                // Create admin for default
                var admin = new Admin[]
                         {
                            new Admin{FisrtName="admin",LastName="admin",UserName="admin",User = new User(){CreateDate = DateTime.Now, UserName = "admin", Password = "123456", Role = 1} }
                          };
                foreach (Admin s in admin)
                {
                    context.Admins.Add(s);
                }
                context.SaveChanges();
            }


        }
    }
}
