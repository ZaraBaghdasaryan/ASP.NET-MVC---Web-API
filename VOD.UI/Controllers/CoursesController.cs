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
    public class CoursesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApiService _api;

        public CoursesController(IApiService api, IMapper mapper)
        {
            _mapper = mapper;
            _api = api;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _api.GetAsync<Course, CourseDTO>(true);
            return View(courses);
        }

         // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id) //Int is nullable => Id.value id must have a value as well
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDTO = await _api.SingleAsync<Course, CourseDTO>(id.Value, true);
            if (courseDTO == null) return NotFound();
            return View(courseDTO);
           
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create() //Showing the Create view
        {
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name"); //Dropdown menu with schools to select to create a new course
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //For security reasons requiring validation of values (schoolId being the one that exists in the database)
        public async Task<IActionResult> Create(CourseDTO course) //Adding the created item from the view
        {
            if (ModelState.IsValid)
            {
                var courseId = await _api.CreateAsync<Course, CourseDTO>(course);
                return RedirectToAction(nameof(Index));
            }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name", course.SchoolId);
            return View(course);
        }

          // GET: Courses/Edit/5
          public async Task<IActionResult> Edit(int? id) //Same here, showing the Edit view
          {
              if (id == null || id.Value < 1)
              {
                  return NotFound();
              }

              var courseDTO = await _api.SingleAsync<Course, CourseDTO>(id.Value, true);
              if (courseDTO == null)
              {
                  return NotFound();
              }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name");
            return View(courseDTO);
          }

         // POST: Courses/Edit/5
          // To protect from overposting attacks, enable the specific properties you want to bind to, for 
          // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(int id, CourseDTO course) //Making the edits in the view
          {
              if (id != course.CourseId)
              {
                  return NotFound();
              }

              if (ModelState.IsValid)
              {
                  try
                  {
                    var success = await _api.UpdateAsync<Course, CourseDTO>(course);
                  }
                  catch 
                  {
                    throw;
                  }
                  return RedirectToAction(nameof(Index));
              }
            var schools = await _api.GetAsync<School, SchoolDTO>(true);
            ViewData["SchoolId"] = new SelectList(schools, "Id", "Name");
            return View(course);
        }

           // GET: Courses/Delete/5
          public async Task<IActionResult> Delete(int? id) //Showing the delete option, allowing to choose
          {
              if (id == null || id.Value < 1)
              {
                  return NotFound();
              }

            var courseDTO = await _api.SingleAsync<Course, CourseDTO>(id.Value, true);
            if (courseDTO == null)
            {
                return NotFound();
            }
            
            return View(courseDTO);
        }

          // POST: Courses/Delete/5
          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken] //Same sender and receiver, samma formulär och samma server
          public async Task<IActionResult> DeleteConfirmed(int id) //Asking for confirmation of delete
          {
            var courseDTO = await _api.SingleAsync<Course, CourseDTO>(id, true);
            if (courseDTO == null) return NotFound();

            var success = await _api.DeleteAsync<Course>(courseDTO.CourseId);

            return RedirectToAction(nameof(Index)); 
          }
    }
}
