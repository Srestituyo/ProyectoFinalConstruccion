using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Indice_Academico.Data;
using Indice_Academico.Data.Entity;
using Indice_Academico.Data.DTO;
using Indice_Academico.Service.Interfaces;

namespace Indice_Academico.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _aSubjectService;
        public SubjectsController(ISubjectService theSubjectService)
        {
            _aSubjectService = theSubjectService;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            
            return View(await _aSubjectService.GetAllSubjects());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aSubject = await _aSubjectService.GetSubject(id.Value);

            return View(aSubject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Name,SubjectCode,Credit,Section,TeacherCode")] CreateSubjectDTO subject)
        {
            if (ModelState.IsValid)
            {
                var aResponse = await _aSubjectService.CreateSubject(subject);

                if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(subject);

            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aSubject = await _aSubjectService.GetSubject(id.Value);
            if (aSubject == null)
            {
                return NotFound();
            }
            return View(aSubject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SubjectCode,Name,Credit, Section, Teacher")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aResponse = await _aSubjectService.UpdateSubject(subject);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _aSubjectService.GetSubject(id.Value);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aResponse = await _aSubjectService.RemoveSubject(id);

            if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(id);
        }
    }
}
