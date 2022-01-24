using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Repository.Interfaces
{
    public interface ISubjectRepository
    {
        Task CreateSubject(Subject theSubject);

        Task<IEnumerable<Subject>> GetAllSubjects();

        Task<Subject> GetSubject(Guid theSubjectId);

        Task UpdateSubject(Subject theUpdatedSubject);

        Task RemoveSubject(Subject theSubject);

        Task<IEnumerable<StudentSubject>> GetStudentSubjects(Guid theStudentId);
    }
}
