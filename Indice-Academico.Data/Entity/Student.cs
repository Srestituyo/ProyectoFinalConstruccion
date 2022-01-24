using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;

namespace Indice_Academico.Data.Entity
{
    public class Student : BaseEntity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int StudentCode { get; set; }

        public string Career { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
