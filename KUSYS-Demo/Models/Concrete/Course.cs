using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Concrete
{
    public class Course : IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        /// <summary>
        /// Course id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Course name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cours code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Enrollments in this course
        /// </summary>
        public ICollection<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// Course fullname
        /// </summary>
        [NotMapped]
        public string FullName { get => $"{Name} ({Code})"; }
    }
}
