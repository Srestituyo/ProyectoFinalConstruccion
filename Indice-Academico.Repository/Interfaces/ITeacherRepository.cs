using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Repository.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddTeacher(Teacher theTeacher);

        Task<RequestStatusReponseModel> UpdateTeacher(Teacher theTeacher);

        Task<Teacher> GetTeacher(Guid theTeacherId);

        Task<Teacher> GetTeacherByCode(int theTeacherCode);

        Task<ICollection<Teacher>> GetAllTeacher(); 

        Task<RequestStatusReponseModel> RemoveTeacher(Guid theTeacherId);
    }
}