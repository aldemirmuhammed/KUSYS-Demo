using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete.ViewModels.StudentUserVMs
{
    public class StudentUser
    {

        public long StudentNo { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }
     
        public string? Password { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Role { get; set; }


    }
}
