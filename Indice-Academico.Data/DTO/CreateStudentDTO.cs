using System;
using System.ComponentModel.DataAnnotations;

namespace Indice_Academico.Data.DTO
{
    public class CreateStudentDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int StudentCode { get; set; }

        [Required]
        public string Career { get; set;} 
         
    }
}
