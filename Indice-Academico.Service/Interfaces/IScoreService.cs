using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indice_Academico.Data.Entity;
using Indice_Academico.Data.DTO;

namespace Indice_Academico.Services.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<StudentDetailsDTO>> GetStudentWithHighestScoreRank();

        Task<bool> ScoreExist(Guid theScore);

        Task<bool> GetScore (Guid theScoreID);

        Task<List<Score>> GetAllScores ();

        Task<Score> UpdateScore(Guid theScoreID);

        Task<bool> CreateNewScore(Score theScore);

        Char ConvertScoreToLetter(int theScore);

        Decimal CalculateHonorScore(int theCredit, string theScore);

        int CalculateTotalCredits(IEnumerable<StudentSubject> theSubjectList);

        int CalculateTotalHonorScore(StudentDetailsDTO theStudentDetailsDTO);

        string CalculateHonorRank(int theTotalHonorValue, int theTotalCreditValue); 

        string CalculateScoreIndex(string theScoreInLetter);
    }
}
