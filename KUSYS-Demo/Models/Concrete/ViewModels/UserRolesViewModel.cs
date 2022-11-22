namespace Models.Concrete.ViewModels
{
    public class UserRolesViewModel
    {
        public User? CurrentUser { get; set; }

        public Student EnrollmentUserByAdmin { get; set; }

        #region Singleton Implementation

        /// <summary>
        /// Singleton lock object
        /// </summary>
        private static readonly object lockObj = new object();

        /// <summary>
        /// Singleton UserRolesViewModel
        /// </summary>
        private static UserRolesViewModel _instance;

        /// <summary>
        /// Instance Property
        /// </summary>
        public static UserRolesViewModel Instance
        {
            get
            {
                lock (lockObj)
                {
                    // Check null reference
                    if (_instance == null)
                    {
                        // Create a new UserRolesViewModel if it is null
                        _instance = new UserRolesViewModel();
                    }
                    return _instance;
                }
            }
            set { _instance = value; }
        }

        #endregion

    }
}
