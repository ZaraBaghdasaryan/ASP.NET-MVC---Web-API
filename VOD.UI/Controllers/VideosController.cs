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
    public class VideosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApiService _api;

        public VideosController(IApiService api, IMapper mapper)
        {
            _mapper = mapper;
            _api = api;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            var Videos = await _api.GetAsync<Video, VideoDTO>(true);
            return View(Videos);
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id) //Int is nullable => Id.value id must have a value as well
        {
            if (id == null)
            {
                return NotFound();
            }

            var VideoDTO = await _api.SingleAsync<Video, VideoDTO>(id.Value, true);
            if (VideoDTO == null) return NotFound();
            return View(VideoDTO);

        }

        // GET: Videos/Create
        public async Task<IActionResult> Create() //Showing the Create view
        {
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name"); //Dropdown menu with Courses to select to create a new Video
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //For security reasons requiring validation of values (CourseId being the one that exists in the database)
        public async Task<IActionResult> Create(VideoDTO Video) //Adding the created item from the view
        {
            if (ModelState.IsValid)
            {
                var VideoId = await _api.CreateAsync<Video, VideoDTO>(Video);
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            return View(Video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id) //Same here, showing the Edit view
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var VideoDTO = await _api.SingleAsync<Video, VideoDTO>(id.Value, true);
            if (VideoDTO == null)
            {
                return NotFound();
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name");
            return View(VideoDTO);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VideoDTO Video) //Making the edits in the view
        {
            if (id != Video.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _api.UpdateAsync<Video, VideoDTO>(Video);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _api.GetAsync<Course, CourseDTO>(true);
            ViewData["CourseId"] = new SelectList(Courses, "Id", "Name");
            return View(Video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id) //Showing the delete option, allowing to choose
        {
            if (id == null || id.Value < 1)
            {
                return NotFound();
            }

            var VideoDTO = await _api.SingleAsync<Video, VideoDTO>(id.Value, true);
            if (VideoDTO == null)
            {
                return NotFound();
            }

            return View(VideoDTO);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Same sender and receiver, samma formulär och samma server
        public async Task<IActionResult> DeleteConfirmed(int id) //Asking for confirmation of delete
        {
            var VideoDTO = await _api.SingleAsync<Video, VideoDTO>(id, true);
            if (VideoDTO == null) return NotFound();

            var success = await _api.DeleteAsync<Video>(VideoDTO.VideoId);

            return RedirectToAction(nameof(Index));
        }
    }
}
