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
    public class StudentRepository: IStudentRepository
    {
        private readonly ApplicationDbContext _theApplicationDbContext;

        public StudentRepository(ApplicationDbContext theApplicationDbContext)
        {
            _theApplicationDbContext = theApplicationDbContext;
        }

        public async Task AddStudent(Student theStudent)
        {
            try
            {
                if (theStudent != null) {
                   await _theApplicationDbContext.AddAsync(theStudent);
                   await _theApplicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<ICollection<Student>> GetAllStudent()
        {
            try
            {
                List<Student> aStudentList = await _theApplicationDbContext.Student.ToListAsync();
                return aStudentList;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Student> GetStudent(Guid theStudentId)
        {
            try
            {
                Student aStudent = await _theApplicationDbContext.Student.FindAsync(theStudentId);
                return aStudent;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Student> GetStudentByCode(int theStudentCode)
        {
            try
            {
                Student aStudent = await _theApplicationDbContext.Student.Where(x => x.StudentCode == theStudentCode).FirstOrDefaultAsync();
                return aStudent; 
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> RemoveStudent(Guid theStudentId)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();

            try
            {
                var aStudent = await _theApplicationDbContext.Student.FirstOrDefaultAsync(x => x.Id == theStudentId);

                if (aStudent != null)
                {
                    _theApplicationDbContext.Student.Remove(aStudent);
                    await _theApplicationDbContext.SaveChangesAsync();
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                    return aResponse;
                }

                aResponse.ErrorMessage = "Estudiante no Existe!";
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                return aResponse;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> UpdateStudent(Student theStudent)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                
                if (theStudent != null)
                {
                    _theApplicationDbContext.Update(theStudent);
                    await _theApplicationDbContext.SaveChangesAsync();
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                }
            }
            catch (Exception ex)
            {
                aResponse.ErrorMessage = ex.Message;
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error; 
            }

            return aResponse;
        }
    }
}
