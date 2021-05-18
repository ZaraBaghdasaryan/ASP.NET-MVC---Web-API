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
    public class ModulesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApiService _api;

        public ModulesController(IApiService api, IMapper mapper)
        {
            _mapper = mapper;
            _api = api;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var Modules = await _api.GetAsync<Module, ModuleDTO>(true);
            return View(Modules);
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id) //Int is nullable => Id.value id must have a value as well
        {
            if (id == null)
            {
                return NotFound();
            }

            var ModuleDTO = await _api.SingleAsync<Module, ModuleDTO>(id.Value, true);
            if (ModuleDTO == null) return NotFound();
            return View(ModuleDTO);

        }

        // GET: Modules/Create
        public async Task<IActionResult> Create() //Showing the Create view
        {
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name"); //Dropdown menu with Courses to select to create a new Module
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //For security reasons requiring validation of values (CourseId being the one that exists in the database)
        public async Task<IActionResult> Create(ModuleDTO Module) //Adding the created item from the view
        {
            if (ModelState.IsValid)
            {
                var ModuleId = await _api.CreateAsync<Module, ModuleDTO>(Module);
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            return View(Module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id) //Same here, showing the Edit view
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var ModuleDTO = await _api.SingleAsync<Module, ModuleDTO>(id.Value, true);
            if (ModuleDTO == null)
            {
                return NotFound();
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name");
            return View(ModuleDTO);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModuleDTO Module) //Making the edits in the view
        {
            if (id != Module.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _api.UpdateAsync<Module, ModuleDTO>(Module);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name");
            return View(Module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id) //Showing the delete option, allowing to choose
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var ModuleDTO = await _api.SingleAsync<Module, ModuleDTO>(id.Value, true);
            if (ModuleDTO == null)
            {
                return NotFound();
            }

            return View(ModuleDTO);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Same sender and receiver, samma formulär och samma server
        public async Task<IActionResult> DeleteConfirmed(int id) //Asking for confirmation of delete
        {
            var ModuleDTO = await _api.SingleAsync<Module, ModuleDTO>(id, true);
            if (ModuleDTO == null) return NotFound();

            var success = await _api.DeleteAsync<Module>(ModuleDTO.ModuleId);

            return RedirectToAction(nameof(Index));
        }
    }
}
