using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Service.Interfaces
{
    public interface IStudentService
    {
       Task<Student> GetStudent(Guid theStudentId);

       Task<ICollection<Student>> GetAllStudents();

       Task<RequestStatusReponseModel> AddStudent(CreateStudentDTO theStudent);

       Task<RequestStatusReponseModel> UpdateStudent(Student theUpdatedStudent);

       Task<RequestStatusReponseModel> RemoveStudent(Guid theStudentId);
    }
}
