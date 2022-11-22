namespace DataAccess.Repository.Abstract
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository{ get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IUserRepository UserRepository { get; }
        IAdminRepository AdminRepository { get; }
        public Task SaveAsync();
    }
}
