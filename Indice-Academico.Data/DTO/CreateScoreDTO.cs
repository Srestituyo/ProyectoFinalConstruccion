using System;
using System.ComponentModel.DataAnnotations;

namespace Indice_Academico.Data.DTO
{
    public class CreateScoreDTO
    {
        [Required]
        public Guid StudentCode { get; set; }

        [Required]
        public Guid TeacherName { get; set; }
        
        [Required]
        public Guid SubjectCode { get; set; }

        [Range(1, 100)]
        [Required]
        public int Score { get; set; }
        
        public string ErrorStatusCode { get; set; }

        public string ErrorMessage {get; set;}
    }
}