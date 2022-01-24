using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Indice_Academico.Data;
using Indice_Academico.Data.DTO;
using Indice_Academico.Services.Interfaces;
using Indice_Academico.Data.Entity;
using Indice_Academico.Service.Interfaces;

namespace Indice_Academico.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IScoreService _aScoreServices;
        private readonly IStudentService _aStudentService;

        public StudentsController(ApplicationDbContext context, IScoreService theScoreService, IStudentService theStudentService)
        { 
            _context = context;
            _aScoreServices = theScoreService;
            _aStudentService = theStudentService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
           return View(await _aStudentService.GetAllStudents());
        }
 

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, StudentCode, Career, ErrorMessage")] CreateStudentDTO theNewStudent)
        {
            if (ModelState.IsValid)
            {
                var aResponse = await _aStudentService.AddStudent(theNewStudent);

                if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Error)
                {
                    ViewBag.Error = aResponse.ErrorMessage;
                    return View(theNewStudent);
                }

                ViewBag.Success = string.Format($"SuccessMessage();");
                return View(theNewStudent);
            }

            return View(theNewStudent);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _aStudentService.GetStudent(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Firstname, Lastname, Career")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aResponse = await _aStudentService.UpdateStudent(student);
                    if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
                    {
                        ViewBag.Success = string.Format($"SuccessMessage();");
                        return View(student);
                    }
                    else {
                        return NotFound();
                    }   
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return NotFound();
                }

            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aStudent = await _aStudentService.GetStudent(id.Value);
            if (aStudent == null)
            {
                return NotFound();
            }

            return View(aStudent);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aResponse = await _aStudentService.RemoveStudent(id);

            if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
            {
                ViewBag.Success = string.Format($"SuccessMessage();");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction($"/Delete/{id}");
        } 
    }
}
