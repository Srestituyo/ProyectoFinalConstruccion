
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
    public class SubjectService: ISubjectService
    {
        private readonly ISubjectRepository _aSubjectRepository;
        private readonly ITeacherService _aTeacherService;

        public SubjectService(ISubjectRepository theSubjecttRepository, ITeacherService theTeacherService)
        {
            _aSubjectRepository = theSubjecttRepository;
            _aTeacherService = theTeacherService;
        }

        public async Task<RequestStatusReponseModel> CreateSubject(CreateSubjectDTO theSubject)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                //Check for Teacher
                var aTeacher = await _aTeacherService.GetTeacherByCode(theSubject.TeacherCode);

                if (aTeacher == null)
                {
                    aResponse.ErrorMessage = "El profesor no existe.";
                    aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Error;

                    return aResponse;
                }

                Subject aNewSubject = new Subject()
                {
                    Credit = theSubject.Credit,
                    Name = theSubject.Name,
                    Section = theSubject.Section,
                    Teacher = aTeacher,
                    TeacherId = aTeacher.Id,
                    SubjectCode = theSubject.SubjectCode,
                    Timestamp = DateTime.UtcNow
                };

                await _aSubjectRepository.CreateSubject(aNewSubject);
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

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            try
            {
                var aListOfSubject = await _aSubjectRepository.GetAllSubjects();

                foreach (var subject in aListOfSubject)
                {
                    var aTeacher = await _aTeacherService.GetTeacher(subject.TeacherId);
                    subject.Teacher = aTeacher;
                }

                return aListOfSubject;
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
                return await _aSubjectRepository.GetStudentSubjects(theStudentId);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Subject> GetSubject(Guid theSubjectId)
        {
            try
            {
                var aSubject = await _aSubjectRepository.GetSubject(theSubjectId);
                aSubject.Teacher = await _aTeacherService.GetTeacher(aSubject.TeacherId);
                return aSubject;
            }   
               
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> RemoveSubject(Guid theSubjectId)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {
                var aSubject = await _aSubjectRepository.GetSubject(theSubjectId);
                await _aSubjectRepository.RemoveSubject(aSubject);

                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aResponse;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<RequestStatusReponseModel> UpdateSubject(Subject theUpdatedSubject)
        {
            RequestStatusReponseModel aResponse = new RequestStatusReponseModel();
            try
            {

                var aTeacher = await _aTeacherService.GetTeacherByCode(theUpdatedSubject.Teacher.TeacherCode);

                theUpdatedSubject.Teacher = aTeacher;
                theUpdatedSubject.TeacherId = aTeacher.Id;
                await _aSubjectRepository.UpdateSubject(theUpdatedSubject);
                aResponse.Status = (int)RequestStatusReponseModel.StatusCode.Ok;
                return aResponse;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
