using System;
namespace Indice_Academico.Data.DTO
{
    public class UpdateStudentDTO
    {
        public Guid StudentId { get; set;  }

        public string FirstName { get; set; }
         
        public string LastName { get; set; }
         
        public string Career { get; set; }
    }
}
