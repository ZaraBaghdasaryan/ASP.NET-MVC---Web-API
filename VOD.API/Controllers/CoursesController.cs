using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using VOD.Common.DTOModels;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Interfaces;

namespace VOD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICRUDService _context; //To implement CRUD for all entities
                                                //without needing to write methods for each GetCustomer, GetCourse etc, just GET...
        private readonly LinkGenerator _linkGenerator;

       public CoursesController(ICRUDService context, LinkGenerator linkGenerator) //Response URI from API for a CRUD operation 
        {
            _context = context;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> GetCourses()
        {
            var courses = await _context.GetAsync<Course, CourseDTO>(); //Find the course. but get out the CourseDTO, True- show navigation property(SchoolId)

            return courses;                     //<Course, CourseDTO>(true) - doesn't work, saying invalid column CourseId (true enables navigation properties- SchoolId to be shown)
        }


        [HttpGet("{id:int}")]

        public async Task<ActionResult<CourseDTO>> Get(int id, bool include = false)
        {
            try
            {
                var dto = await _context.SingleAsync<Course, CourseDTO>(s => s.Id.Equals(id), include); // Retrieve courseDto with an Id
                if (dto == null) return NotFound(); //If was null then show NotFound
                return dto; //Otherwise return the dto
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure"); //If an error occurred in our server and had nothing to do with the user, then show internal server error 
            }
        }

        [HttpPost]

        public async Task<ActionResult<CourseDTO>> Post(CourseDTO model) //Creating a course through API- using a DTO and a model instance of it
        {
            try
            {
                if (model == null) return BadRequest("No entity provided"); //If the user hasn't provided any data then BadRequest

                var exists = await _context.AnyAsync<School>(a => a.Id.Equals(model.SchoolId)); //If user provided, but we need to check that the navigation property (School Id for the course) exists in the database
                if (!exists) return NotFound("Could not find related entity.");

                var id = await _context.CreateAsync<CourseDTO, Course>(model); //Then we need to check that id generated is not smaller than 1
                if (id < 1) return BadRequest("Unable to add the entity.");

                var dto = await _context.SingleAsync<Course, CourseDTO>(s => s.Id.Equals(id), true); //Then we try to find what has been created with SingleAsync
                if (dto == null) return BadRequest("Unable to add the entity.");

                var uri = _linkGenerator.GetPathByAction("Get", "Books", new {id}); //And once it has been created the Link Generator uses the Created Microsoft method to return the uri based on parameters "Get", "Books", new {id})
                return Created(uri, dto); //Created similar to 201 status ok
            }

            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the entity"); //And if there are errors that the user is not responsible for then we throw an internal server error due to it occurring in our server 
            }
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult<CourseDTO>> Put(int id, CourseDTO model) //Creating a course through API- using a DTO and a model instance of it
        {
            try
            {
                if (model == null) return BadRequest("No entity provided"); //If the user hasn't provided any data then BadRequest
                if (!id.Equals(model.CourseId)) return BadRequest("Differing Ids.");

                var exists = await _context.AnyAsync<School>(a => a.Id.Equals(model.SchoolId)); //If user provided, but we need to check that the navigation property (School Id for the course) exists in the database
                if (!exists) return NotFound("Could not find related entity.");

                exists = await _context.AnyAsync<Course>(a => a.Id.Equals(id));
                if (!exists) return NotFound("Could not find entity.");

                if (await _context.UpdateAsync<CourseDTO, Course>(model)) return NoContent(); //NoContent- nothing to show, but succeeded in updating the entity
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the entity"); //And if there are errors that the user is not responsible for then we throw an internal server error due to it occurring in our server 
            }

            return BadRequest("Unable to update the entity.");
        }

        [HttpDelete("{id:int}")]
       

        public async Task<ActionResult> Delete(int id) //Creating a course through API- using a DTO and a model instance of it
        {
            try
            {
                var exists = await _context.AnyAsync<Course>(a => a.Id.Equals(id)); //If the user hasn't provided any data then BadRequest
                if (!exists) return BadRequest("Could not find related entity.");

                if (await _context.DeleteAsync<Course>(d => d.Id.Equals(id))) return NoContent(); //NoContent- nothing to show, but succeeded in updating the entity
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the entity"); //And if there are errors that the user is not responsible for then we throw an internal server error due to it occurring in our server 
            }

            return BadRequest("Failed to delete the entity.");
        }

        /*    // GET: api/Courses/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Course>> GetCourse(int id)
            {
                var course = await _context.Courses.FindAsync(id);

                if (course == null)
                {
                    return NotFound();
                }

                return course;
            }

            // PUT: api/Courses/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCourse(int id, Course course)
            {
                if (id != course.Id)
                {
                    return BadRequest();
                }

                _context.Entry(course).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Courses
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<Course>> PostCourse(Course course)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            }

            // DELETE: api/Courses/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCourse(int id)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return NotFound();
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool CourseExists(int id)
            {
                return _context.Courses.Any(e => e.Id == id);
            } */
    }
}


/* 
Istället för att en IMapper och VODContext kan vi kan vi ha CRUD services 
och universella CRUD operationer för varje entitet
private readonly VODContext _context;
private readonly IMapper _mapper; 

public CoursesController(VODContext context, IMapper mapper)
{
    _context = context; //Accessing the database context through _context box/variable
    _mapper = mapper; //USing auto mapper through _mapper box
}
*/