using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            Admins = new HashSet<Admin>();
            Students = new HashSet<Student>();
        }

        /// <summary>
        /// Student id
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// For Admin username , for student student no
        /// </summary>
        [Required]
        public string? UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        /// <summary>
        /// Create date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public int? Role { get; set; }


        public  ICollection<Admin> Admins { get; set; }
        public  ICollection<Student> Students { get; set; }

    }

}
