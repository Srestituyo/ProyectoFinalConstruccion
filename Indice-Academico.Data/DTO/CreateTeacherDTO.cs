using System.ComponentModel.DataAnnotations;


namespace Indice_Academico.Data.DTO
{
    public class CreateTeacherDTO
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int TeacherCode { get; set; }
    }
}
