using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Concrete
{
    public class Student : IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        /// <summary>
        /// Student id
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Student no
        /// </summary>
        public string StudentNo { get; set; } 

        /// <summary>
        /// Student name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Student last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Student username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Student password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Student birth date
        /// </summary>
        public DateTime BirthDate { get; set; }


        /// <summary>
        /// User FK
        /// </summary>
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        /// <summary>
        /// Enrollments in student
        /// </summary>
        public ICollection<Enrollment> Enrollments { get; set; }



        [NotMapped]
        public string FullName { get => $"{Name} {LastName}"; }
    }
}