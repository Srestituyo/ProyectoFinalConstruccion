using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Indice_Academico.Data; 
using Indice_Academico.Models; 
using Indice_Academico.Data.DTO;
using Indice_Academico.Services.Interfaces;
using Indice_Academico.Data.Entity;

namespace Indice_Academico.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IScoreService _aScoreService;

        public ScoresController(ApplicationDbContext context, IScoreService theScoreService)
        {
            _aScoreService = theScoreService;
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
            var aScoreList = await _aScoreService.GetStudentWithHighestScoreRank();
            return View(aScoreList.AsEnumerable());
        }

        // GET: Scores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .FirstOrDefaultAsync(m => m.Id == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Scores/Create
        public IActionResult Create()
        {
            //Lets Fill up are ViewModel
            var aScoreViewModel = new CreateScoreDTO();

            var aTeacherList = _context.Teacher.AsEnumerable();
            var aStudenList =  _context.Student.AsEnumerable();
            var aSubjectList = _context.Subject.AsEnumerable();

            ViewBag.TeacherList = aTeacherList;
            ViewBag.StudentList = aStudenList;
            ViewBag.SubjectList = aSubjectList;



            return View(aScoreViewModel);
        }



        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,TeacherName,SubjectCode,Score")] CreateScoreDTO theCreateScore)
        {
            var aTeacherList = _context.Teacher.AsEnumerable();
            var aStudenList = _context.Student.AsEnumerable();
            var aSubjectList = _context.Subject.AsEnumerable();
            if (ModelState.IsValid)
            {

                var aStudent = _context.Student.Where(x => x.Id == theCreateScore.StudentCode).FirstOrDefault();

                if (aStudent == null)
                {
                    theCreateScore.ErrorMessage = "El Codigo de Estudiante no Existe.";
                    return View(theCreateScore);
                }

                Guid aSubjectId = _context.Subject.Where(x => x.Id == theCreateScore.SubjectCode).FirstOrDefault().Id;

                if (aSubjectId == Guid.Empty)
                {
                    theCreateScore.ErrorMessage = "El codigo de la asignacion no existe.";
                    return View(theCreateScore);
                }

                var aTeacher = _context.Teacher.Where(x => x.Id == theCreateScore.TeacherName).FirstOrDefault();
                if (aTeacher == null)
                {
                    theCreateScore.ErrorMessage = "El profesor no existe."; 
                    return View(theCreateScore);
                }

                Score aNewScore = new Score()
                {
                    //Grade = theCreateScore.Score.ToString(),
                    //GradeId = Guid.NewGuid(),
                    //StudentId = aStudent.Id,
                    //SubjectId = aSubjectId,
                    //TeacherId = aTeacher.Id
                };

                _context.Add(aNewScore);
                await _context.SaveChangesAsync();

                ViewBag.Success = string.Format($"SuccessMessage('Codigo de Estudiante no Existe');");
                ViewBag.TeacherList = aTeacherList;
                ViewBag.StudentList = aStudenList;
                ViewBag.SubjectList = aSubjectList;
                return View(theCreateScore);

            }
           

            ViewBag.TeacherList = aTeacherList;
            ViewBag.StudentList = aStudenList;
            ViewBag.SubjectList = aSubjectList;

            return View(theCreateScore);
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GradeId,StudentId,SubjectId,TeacherId,Grade")] Score score)
        {
            if (id != score.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(score);
        }

        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .FirstOrDefaultAsync(m => m.Id == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var score = await _context.Score.FindAsync(id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(Guid id)
        {
            return _context.Score.Any(e => e.Id == id);
        }
        private ScoreViewModel GetScoreViewModel()
        {
            ScoreViewModel aScoreViewModel = new ScoreViewModel();

            //var aSubjectList = _context.Subject.ToList();
            //aSubjectList.ForEach(x =>
            //   aScoreViewModel.Subjects.Add(x)
            //);

            //var aTeacherList = _context.Teacher.ToList();
            //aTeacherList.ForEach(x =>
            //    aScoreViewModel.Teachers.Add(x)
            //);

            //var aStudentList = _context.Student.ToList();
            //aStudentList.ForEach(x =>
            //    aScoreViewModel.Students.Add(x)
            //);

            return aScoreViewModel;
        }


    }
}
