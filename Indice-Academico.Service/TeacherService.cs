using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;
using Indice_Academico.Repository.Interfaces;
using Indice_Academico.Service.Interfaces;

namespace Indice_Academico.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _theTeacherRepository;
        public TeacherService(ITeacherRepository theTeacherRepository)
        {
            _theTeacherRepository = theTeacherRepository;
        }

        public async Task<RequestStatusReponseModel> AddTeacher(CreateTeacherDTO theTeacher)
        {
            try
            {
                RequestStatusReponseModel aReponse = new RequestStatusReponseModel();

                if (theTeacher != null)
                {
                    Teacher aTeacher = await _theTeacherRepository.GetTeacherByCode(theTeacher.TeacherCode);

                    if (aTeacher != null)
                    {
                        aReponse.ErrorMessage = "Ya existe un profesor con este Codigo!";
                        aReponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                        return aReponse;
                    }

                    var aNewTeacher = new Teacher()
                    {
                        Firstname = theTeacher.Firstname,
                        Lastname = theTeacher.Lastname,
                        TeacherCode = theTeacher.TeacherCode,
                        Timestamp = DateTime.UtcNow
                    };

                    await _theTeacherRepository.AddTeacher(aNewTeacher);
                }

                aReponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aReponse;

            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<ICollection<Teacher>> GetAllTeachers()
        {
            try
            {
                ICollection<Teacher> aTeacherList = await _theTeacherRepository.GetAllTeacher();
                return aTeacherList;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<Teacher> GetTeacher(Guid theTeacherId)
        {
            return await _theTeacherRepository.GetTeacher(theTeacherId);
        }

        public async Task<Teacher> GetTeacherByCode(int theTeacherCode)
        {
          return await _theTeacherRepository.GetTeacherByCode(theTeacherCode);
        }

        public async Task<RequestStatusReponseModel> RemoveTeacher(Guid theTeacherId)
        {
            return await _theTeacherRepository.RemoveTeacher(theTeacherId);
        }

        public async Task<RequestStatusReponseModel> UpdateTeacher(Teacher theUpdateTeacher)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();

            try
            {
                Teacher aTeacher = await _theTeacherRepository.GetTeacher(theUpdateTeacher.Id);

                if (aTeacher == null)
                {
                    aResponse.ErrorMessage = "El Profesor no se pudo encontrar.";
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;
                    return aResponse;
                }

                aTeacher.Firstname = theUpdateTeacher.Firstname;
                aTeacher.Lastname = theUpdateTeacher.Lastname;
                aTeacher.Subjects = theUpdateTeacher.Subjects;

                aResponse = await _theTeacherRepository.UpdateTeacher(aTeacher);
                return aResponse;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }


    }
}
