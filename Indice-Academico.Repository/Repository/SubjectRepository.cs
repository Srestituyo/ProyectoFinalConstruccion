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
    public class SubjectRepository: ISubjectRepository
    {
        private readonly ApplicationDbContext _aApplicationDbContext;

        public SubjectRepository(ApplicationDbContext theApplicationDbContext)
        {
            _aApplicationDbContext = theApplicationDbContext;
        }

        public async Task CreateSubject(Subject theSubject)
        {
            try
            {
                _aApplicationDbContext.Add(theSubject);
                await _aApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            try
            {
                return await  _aApplicationDbContext.Subject.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<StudentSubject>> GetStudentSubjects(Guid theStudentId)
        {
            try
            {
                return await _aApplicationDbContext.StudentSubjects.Where(x => x.Id == theStudentId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public Task<Subject> GetSubject(Guid theSubjectId)
        {
            try
            {
                return _aApplicationDbContext.Subject.FirstOrDefaultAsync(x => x.Id == theSubjectId);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task RemoveSubject(Subject theSubject)
        {
            try
            {
                _aApplicationDbContext.Remove(theSubject);
                await _aApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task UpdateSubject(Subject theUpdatedSubject)
        {
            try
            {
                _aApplicationDbContext.Update(theUpdatedSubject);
                await _aApplicationDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
