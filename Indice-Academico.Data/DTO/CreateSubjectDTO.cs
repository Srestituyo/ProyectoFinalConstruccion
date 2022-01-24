using System;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Data.DTO
{
    public class CreateSubjectDTO
    {
        public string Name { get; set; }

        public string SubjectCode { get; set; }

        public int Credit { get; set; }

        public int Section { get; set; }

        public int TeacherCode { get; set; }
    }
}
