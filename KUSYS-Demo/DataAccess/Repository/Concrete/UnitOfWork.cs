
using DataAccess.Repository.Abstract;

namespace DataAccess.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;

        /// <summary>
        /// Constructor of Unitofwork
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(KUSYSDbContext db)
        {
            _db = db;
            StudentRepository = new StudentRepository(_db);
            CourseRepository = new CourseRepository(_db);
            EnrollmentRepository = new EnrollmentRepository(_db);
            UserRepository = new UserRepository(_db);
            AdminRepository = new AdminRepository(_db);
        }

        /// <summary>
        /// Create repository interface of object
        /// </summary>
        public IStudentRepository StudentRepository{ get; private set; }
        public ICourseRepository CourseRepository{ get; private set; }
        public IEnrollmentRepository EnrollmentRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IAdminRepository AdminRepository { get; private set; }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
