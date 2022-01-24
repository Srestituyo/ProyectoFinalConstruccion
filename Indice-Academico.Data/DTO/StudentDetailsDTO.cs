using System;
using System.Collections.Generic;

namespace Indice_Academico.Data.DTO
{
    public class StudentDetailsDTO
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int StudentCode { get; set; }

        public string Career { get; set; }

        public int TotalCredit { get; set; }

        public int TotalHonorScore { get; set; }

        public List<SubjectDTO> SubjectList { get; set;}
    }
}
