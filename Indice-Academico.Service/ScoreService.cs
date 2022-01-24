using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Data.Entity;
using Indice_Academico.Repository.Interfaces;
using Indice_Academico.Service.Interfaces;
using Indice_Academico.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Indice_Academico.Services
{
    public class ScoreServices : IScoreService
    {
        private readonly IStudentService _aStudentService;
        private readonly ISubjectService _aSubjectService;
        private readonly IScoreRepository _aScoreRepository;

        public ScoreServices(IStudentService theStudentService, ISubjectService theSubjectService, IScoreRepository theScoreRepository) 
        {
            _aStudentService = theStudentService;
            _aSubjectService = theSubjectService;
            _aScoreRepository = theScoreRepository;
        }

        public async Task<IEnumerable<StudentDetailsDTO>> GetStudentWithHighestScoreRank()
        {
            try
            {
                var aReturnList = new List<StudentDetailsDTO>();
                Guid aStudentId = Guid.Empty;

                var aSubjectList = await _aSubjectService.GetAllSubjects();

                foreach (var aSubject in aSubjectList)
                {
                    var aSubjectScoreList = await _aScoreRepository.GetSubjectScore(aSubject.Id);

                    int aGrade = 0;
                    foreach (var aScore in aSubjectScoreList)
                    {
                        if (aGrade <= aScore.ScoreInNumber)
                        {
                            aGrade = aScore.ScoreInNumber;
                            aStudentId = aScore.StudentId;
                        }
                    }

                    if (aStudentId != Guid.Empty)
                    {
                        var aStudentList = await _aStudentService.GetAllStudents();
                        var aStudent = aStudentList.Where(x => x.Id == aStudentId).FirstOrDefault();

                        var aStudentDetail = new StudentDetailsDTO()
                        {
                            Firstname = aStudent.Firstname,
                            Lastname = aStudent.Lastname,
                            StudentCode = aStudent.StudentCode,
                        };

                        var aScoreInLetter = ConvertScoreToLetter(aGrade).ToString();
                        aStudentDetail.SubjectList = new List<SubjectDTO>();

                        aStudentDetail.SubjectList.Add(new SubjectDTO()
                        {
                            Credit = aSubject.Credit,
                            Grade = aGrade.ToString(),
                            GradeInLetter = aScoreInLetter,
                            SubjectCode = aSubject.SubjectCode,
                            Name = aSubject.Name,
                            HonorRank = CalculateScoreIndex(aScoreInLetter),
                            HonorScore = 0
                        });

                        aReturnList.Add(aStudentDetail);
                    }
                }

                return aReturnList;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public string CalculateHonorRank(int theTotalHonorValue, int theTotalCreditValue)
        {
            string aHonorRank = string.Empty;
            int aGPAScore = theTotalHonorValue / theTotalCreditValue;

            if (aGPAScore >= 3.8 && aGPAScore <= 4.0)
            {
                aHonorRank = "Summa Cum Laude";
            }
            else if (aGPAScore >= 3.5 && aGPAScore <= 3.7)
            {
                aHonorRank = "Magna Cum Laude";
            }
            else if (aGPAScore >= 3.2 && aGPAScore <= 3.4)
            {
                aHonorRank = "Cum Laude";
            }
            else if(aGPAScore <= 3.1)
            {
                aHonorRank = "Sin honor";
            }

            return aHonorRank;
        }

        public Decimal CalculateHonorScore(int theCredit, string theScore)
        {
            return Convert.ToDecimal(theCredit) * Convert.ToDecimal(theScore);
        }

        public int CalculateTotalCredits(IEnumerable<StudentSubject> theSubjectList)
        {
            int aTotalOfCredits = 0;
            foreach (var aSubject in theSubjectList)
            {
                aTotalOfCredits += aSubject.Subject.Credit;
            }

            return aTotalOfCredits;
        }

        public int CalculateTotalHonorScore(StudentDetailsDTO theStudentDetailsDTO)
        {
            int aTotalHonorScore = 0;
            foreach (var aStudentSubject in theStudentDetailsDTO.SubjectList)
            {
                aTotalHonorScore += aStudentSubject.HonorScore;
            }

            return aTotalHonorScore;
        }

        public Char ConvertScoreToLetter(int theScore)
        {
           var aScoreLetter = ' ';
            if(theScore >= 90 && theScore <= 100)
            {
                aScoreLetter = 'A';
            }
            else if(theScore >= 80 && theScore <= 89)
            {
                 aScoreLetter = 'B';
            }
            else if (theScore >= 70 && theScore <= 79 )
            {
                aScoreLetter = 'C';
            }
            else if(theScore >= 60 && theScore <= 69)
            {
                aScoreLetter = 'D';
            }
            else if (aScoreLetter <= 59)
            {
                aScoreLetter = 'F';
            }

            return aScoreLetter;
        }

        public string CalculateScoreIndex(string theScoreInLetter)
        {
            if (theScoreInLetter == "A")
            {
                return "4.0";
            }
            else if (theScoreInLetter == "B")
            {
                return "3.0";
            }
            else if (theScoreInLetter == "C")
            {
                return "2.0";
            }
            else if (theScoreInLetter == "D")
            {
                return "1.0";
            }
            else if (theScoreInLetter == "F")
            {
                return "0.0";
            }

            return "0.0";
        }

        public Task<bool> ScoreExist(Guid theScore)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetScore(Guid theScoreID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Score>> GetAllScores()
        {
            throw new NotImplementedException();
        }

        public Task<Score> UpdateScore(Guid theScoreID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateNewScore(Score theScore)
        {
            throw new NotImplementedException();
        }
    }
}
