using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Indice_Academico.Data.DTO
{
    public class AsignSubjectDTO
    {
        [Required]
        public string SubjectCode { get; set; } 
        [Required]
        public int StudentCode { get; set; }

        public Guid StudentID { get; set; }

        public string ErrorMessage { get; set; }
    }
}
