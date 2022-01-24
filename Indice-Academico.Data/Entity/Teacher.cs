using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;

namespace Indice_Academico.Data.Entity
{
    public class Teacher: BaseEntity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int TeacherCode { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }
}
