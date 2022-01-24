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
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITeacherService _aTeacherService;

        public TeachersController(ApplicationDbContext context, ITeacherService theTeacherService)
        {
            _context = context;
            _aTeacherService = theTeacherService;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _aTeacherService.GetAllTeachers());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _aTeacherService.GetTeacher(id.Value);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,TeacherCode")] CreateTeacherDTO theNewTeacher)
        {
            if (ModelState.IsValid)
            {
                var aResponse = await _aTeacherService.AddTeacher(theNewTeacher);

                if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Error)
                {
                    ViewBag.Error = aResponse.ErrorMessage;
                    return View(theNewTeacher);
                }

                ViewBag.Success = string.Format($"SuccessMessage();");
                return RedirectToAction(nameof(Index));

            }
            return View(theNewTeacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _aTeacherService.GetTeacher(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Firstname,Lastname,TeacherCode")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aResponse = await _aTeacherService.UpdateTeacher(teacher);
                    if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
                    {
                        ViewBag.Success = string.Format($"SuccessMessage();");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _aTeacherService.GetTeacher(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var aResponse = await _aTeacherService.RemoveTeacher(id);


            if (aResponse.Status == (int)RequestStatusReponseModel.StatusCode.Ok)
            {
                ViewBag.Success = string.Format($"SuccessMessage();");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction($"/Delete/{id}");
        }

        private bool TeacherExists(int theTeacherCode)
        {
            return _context.Teacher.Any(e => e.TeacherCode == theTeacherCode);
        }
    }
}
