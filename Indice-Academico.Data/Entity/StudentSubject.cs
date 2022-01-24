using System;
namespace Indice_Academico.Data.Entity
{
    public class StudentSubject: BaseEntity
    {
        public string ScoreInLetter { get; set; }

        public int ScoreInNumber { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
