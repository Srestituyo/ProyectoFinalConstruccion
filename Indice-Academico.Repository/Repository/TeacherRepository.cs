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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _theApplicationDbContext;
        public TeacherRepository(ApplicationDbContext theApplicationDbContext)
        {
            _theApplicationDbContext = theApplicationDbContext;
        }

        public async Task AddTeacher(Teacher theTeacher)
        {
            try
            {
                if (theTeacher != null)
                {
                    await _theApplicationDbContext.AddAsync(theTeacher);
                    await _theApplicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<ICollection<Teacher>> GetAllTeacher()
        {
            try
            {
                List<Teacher> aTeacherList = await _theApplicationDbContext.Teacher.ToListAsync();
                return aTeacherList;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Teacher> GetTeacher(Guid theTeacherId)
        {
            try
            {
                Teacher aTeacher = await _theApplicationDbContext.Teacher.FindAsync(theTeacherId);
                return aTeacher;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<Teacher> GetTeacherByCode(int theTeacherCode)
        {
            try
            {
                Teacher aTeacher = await _theApplicationDbContext.Teacher.Where(x => x.TeacherCode == theTeacherCode).FirstOrDefaultAsync();
                return aTeacher;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> RemoveTeacher(Guid theTeacherId)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();

            try
            {
                var aTeacher = await _theApplicationDbContext.Teacher.FirstOrDefaultAsync(x => x.Id == theTeacherId);

                if (aTeacher != null)
                {
                    _theApplicationDbContext.Teacher.Remove(aTeacher);
                    await _theApplicationDbContext.SaveChangesAsync();
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                    return aResponse;
                }

                aResponse.ErrorMessage = "Profesor no Existe!";
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                return aResponse;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> UpdateTeacher(Teacher theTeacher)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                if (theTeacher != null)
                {
                    _theApplicationDbContext.Update(theTeacher);
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