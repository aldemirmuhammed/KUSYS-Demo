using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Concrete
{
    public class Admin :  IEntity 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Admin()
        {
            User = new User();
        }

        /// <summary>
        /// Admin id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Admin username
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Admin first name
        /// </summary>
        public string? FisrtName { get; set; }

        /// <summary>
        /// Admin lastname
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Admin FK
        /// </summary>
        public int UserId { get; set; }

        public  User User { get; set; } = null!;

        [NotMapped]
        public string FullName { get => $"{FisrtName} {LastName}"; }

    }
}
