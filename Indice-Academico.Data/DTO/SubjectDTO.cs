using System;
namespace Indice_Academico.Data.DTO
{
    public class SubjectDTO
    {
        public string SubjectCode { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public string Grade { get; set; }

        public string GradeInLetter { get; set;}

        public string HonorRank { get; set; }

        public int HonorScore { get; set; } 
     
    }
}
