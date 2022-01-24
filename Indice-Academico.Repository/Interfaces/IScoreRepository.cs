using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Repository.Interfaces
{
    public interface IScoreRepository
    {
        Task<RequestStatusReponseModel> AddNewScore(StudentSubject theScore);

        Task<ICollection<StudentSubject>> GetStudentScore(Guid theStudentId, Guid theSubjectId);

        Task<RequestStatusReponseModel> UpdateSubjectScore(StudentSubject theUpdatedScore);

        Task<ICollection<StudentSubject>> GetSubjectScore(Guid theSubjectId);
    }
}