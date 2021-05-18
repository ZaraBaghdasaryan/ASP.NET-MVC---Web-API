using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOD.Common.DTOModels;
using VOD.Common.Entities;
using VOD.Common.Services;
using VOD.Database.Contexts;

namespace VOD.UI.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApiService _api;

        public InstructorsController(IApiService api, IMapper mapper)
        {
            _mapper = mapper;
            _api = api;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var Instructors = await _api.GetAsync<Instructor, InstructorDTO>(true);
            return View(Instructors);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id) //Int is nullable => Id.value id must have a value as well
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstructorDTO = await _api.SingleAsync<Instructor, InstructorDTO>(id.Value, true);
            if (InstructorDTO == null) return NotFound();
            return View(InstructorDTO);

        }

        // GET: Instructors/Create
        public async Task<IActionResult> Create() //Showing the Create view
        {
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name"); //Dropdown menu with schools to select to create a new Instructor
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //For security reasons requiring validation of values (schoolId being the one that exists in the database)
        public async Task<IActionResult> Create(InstructorDTO Instructor) //Adding the created item from the view
        {
            if (ModelState.IsValid)
            {
                var InstructorId = await _api.CreateAsync<Instructor, InstructorDTO>(Instructor);
                return RedirectToAction(nameof(Index));
            }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            return View(Instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id) //Same here, showing the Edit view
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var InstructorDTO = await _api.SingleAsync<Instructor, InstructorDTO>(id.Value, true);
            if (InstructorDTO == null)
            {
                return NotFound();
            }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name");
            return View(InstructorDTO);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorDTO Instructor) //Making the edits in the view
        {
            if (id != Instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _api.UpdateAsync<Instructor, InstructorDTO>(Instructor);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name");
            return View(Instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id) //Showing the delete option, allowing to choose
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var InstructorDTO = await _api.SingleAsync<Instructor, InstructorDTO>(id.Value, true);
            if (InstructorDTO == null)
            {
                return NotFound();
            }

            return View(InstructorDTO);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Same sender and receiver, samma formulär och samma server
        public async Task<IActionResult> DeleteConfirmed(int id) //Asking for confirmation of delete
        {
            var InstructorDTO = await _api.SingleAsync<Instructor, InstructorDTO>(id, true);
            if (InstructorDTO == null) return NotFound();

            var success = await _api.DeleteAsync<Instructor>(InstructorDTO.InstructorId);

            return RedirectToAction(nameof(Index));
        }
    }
}
