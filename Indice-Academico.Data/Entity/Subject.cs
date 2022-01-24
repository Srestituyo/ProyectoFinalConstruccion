using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;

namespace Indice_Academico.Data.Entity
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public string SubjectCode { get; set; }

        public int Credit { get; set; }

        public int Section { get; set; } 

        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; } 
    }
}
