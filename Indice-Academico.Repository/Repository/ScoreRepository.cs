using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.Entity;
using Indice_Academico.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indice_Academico.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _aApplicationDbContext;


        public ScoreRepository(ApplicationDbContext theApplicationDbContext)
        {
            _aApplicationDbContext = theApplicationDbContext;
        }

        public async Task<RequestStatusReponseModel> AddNewScore(StudentSubject theScore)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                await _aApplicationDbContext.AddAsync(theScore);
                await _aApplicationDbContext.SaveChangesAsync();

                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aResponse;
                
            }
            catch (Exception ex)
            {
                aResponse.ErrorMessage = ex.Message;
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                return aResponse;
            }
        }

        public async Task<ICollection<StudentSubject>> GetStudentScore(Guid theStudentId, Guid theSubjectId)
        {
            try
            {
                return await _aApplicationDbContext.StudentSubjects.Where(x=>x.StudentId == theStudentId && x.SubjectId == theSubjectId ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }   
        }

        public async Task<ICollection<Subject>> GetSubjectScore(Guid theSubjectId)
        {
            try
            {
                return await _aApplicationDbContext.Subject.Where(x => x.Id == theSubjectId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> UpdateSubjectScore(StudentSubject theUpdatedScore)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                _aApplicationDbContext.Update(theUpdatedScore);
                await _aApplicationDbContext.SaveChangesAsync();

                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aResponse;
            }
            catch (Exception ex)
            {
                aResponse.ErrorMessage = ex.Message;
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aResponse;
            }
        }

        Task<ICollection<StudentSubject>> IScoreRepository.GetSubjectScore(Guid theSubjectId)
        {
            throw new NotImplementedException();
        }
    }
}