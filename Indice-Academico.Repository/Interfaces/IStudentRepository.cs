using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task AddStudent(Student theStudent);

        Task<RequestStatusReponseModel> UpdateStudent(Student theStudent);

        Task<Student> GetStudent(Guid theStudentiD);

        Task<ICollection<Student>> GetAllStudent();

        Task<Student> GetStudentByCode(int theStudentCode);

        Task<RequestStatusReponseModel> RemoveStudent(Guid theStudentId);
    }
}
