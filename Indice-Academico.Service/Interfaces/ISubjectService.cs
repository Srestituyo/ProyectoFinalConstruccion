using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Service.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject> GetSubject(Guid theSubjectId);

        Task<IEnumerable<Subject>> GetAllSubjects();

        Task<RequestStatusReponseModel> CreateSubject(CreateSubjectDTO theSubject);

        Task<RequestStatusReponseModel> UpdateSubject(Subject theUpdatedSubject);

        Task<RequestStatusReponseModel> RemoveSubject(Guid theSubjectId);

        Task<IEnumerable<StudentSubject>> GetStudentSubjects(Guid theStudentId);
    }
}
