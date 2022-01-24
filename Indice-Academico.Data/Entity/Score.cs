using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;

namespace Indice_Academico.Data.Entity
{
    public class Score : BaseEntity
    {
        public int GradeNumber { get; set; }

        public string GradeLetter { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher {get; set;}
    }
}
