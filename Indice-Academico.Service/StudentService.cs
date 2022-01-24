using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;
using Indice_Academico.Repository.Interfaces;
using Indice_Academico.Service.Interfaces;
using Indice_Academico.Services.Interfaces;

namespace Indice_Academico.Service
{
    public class StudentService: IStudentService
    {

        private readonly IStudentRepository _theStudentRepository;
        private readonly ISubjectService _aSubjectService; 

        public StudentService(IStudentRepository theStudentRepository, ISubjectService theSubjectService)
        {
            _theStudentRepository = theStudentRepository;
            _aSubjectService = theSubjectService;
             
        }

        public async Task<RequestStatusReponseModel> AddStudent(CreateStudentDTO theStudent)
        {
            try
            {
                RequestStatusReponseModel aReponse = new RequestStatusReponseModel();

                if (theStudent != null)
                {
                    //checking if exists
                    Student aStudent = await _theStudentRepository.GetStudentByCode(theStudent.StudentCode);

                    if (aStudent != null) {
                        aReponse.ErrorMessage = "Ya existe un estudiante con esta matricula!";
                        aReponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                        return aReponse;
                    } 

                    var aNewStudent = new Student() {

                        Firstname = theStudent.FirstName,
                        Lastname = theStudent.LastName,
                        StudentCode = theStudent.StudentCode,
                        Career = theStudent.Career,
                        Timestamp = DateTime.UtcNow
                    };

                    await _theStudentRepository.AddStudent(aNewStudent);
                }

                aReponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aReponse;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<ICollection<Student>> GetAllStudents()
        {
            try
            {
                ICollection<Student> aStudentList = await _theStudentRepository.GetAllStudent();
                return aStudentList;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Student> GetStudent(Guid theStudentId)
        {
           return await _theStudentRepository.GetStudent(theStudentId);
        }

   

        public async Task<RequestStatusReponseModel> RemoveStudent(Guid theStudentId)
        {
           return await _theStudentRepository.RemoveStudent(theStudentId);
        }

        public async Task<RequestStatusReponseModel> UpdateStudent(Student theUpdatedStudent)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();

            try
            {
                Student aStudent = await _theStudentRepository.GetStudent(theUpdatedStudent.Id);

                if (aStudent == null)
                {
                    aResponse.ErrorMessage = "El Estudiante no se pudo encontrar.";
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;
                    return aResponse;
                }

                aStudent.Firstname = theUpdatedStudent.Firstname;
                aStudent.Lastname = theUpdatedStudent.Lastname;
                aStudent.Career = theUpdatedStudent.Career;

                aResponse = await _theStudentRepository.UpdateStudent(aStudent);
                return aResponse;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
                
        }
    }
}
