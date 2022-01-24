using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Service.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> GetTeacher(Guid theTeacherId);

        Task<Teacher> GetTeacherByCode(int theTeacherCode);

        Task<ICollection<Teacher>> GetAllTeachers();

        Task<RequestStatusReponseModel> AddTeacher(CreateTeacherDTO theTeacher);

        Task<RequestStatusReponseModel> UpdateTeacher(Teacher theUpdateTeacher);

        Task<RequestStatusReponseModel> RemoveTeacher(Guid theTeachertId);
    }
}
