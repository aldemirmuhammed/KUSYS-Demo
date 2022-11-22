using Core.Entities;

namespace Models.Concrete
{
    public class Enrollment : IEntity
    {
        /// <summary>
        /// Enrollment id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student FK
        /// </summary>
        public int? StudentId { get; set; }

        /// <summary>
        /// Course FK
        /// </summary>
        public int? CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
