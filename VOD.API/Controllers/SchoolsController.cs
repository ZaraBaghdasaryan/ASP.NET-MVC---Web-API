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
    public class SchoolsController : ControllerBase
    {
        private readonly ICRUDService _context; //To implement CRUD for all entities
                                                //without needing to write methods for each GetCustomer, GetSchool etc, just GET...
        private readonly LinkGenerator _linkGenerator;

       public SchoolsController(ICRUDService context, LinkGenerator linkGenerator) //Response URI from API for a CRUD operation 
        {
            _context = context;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<IEnumerable<SchoolDTO>> GetSchools()
        {
            var Schools = await _context.GetAsync<School, SchoolDTO>(); //Find the School. but get out the SchoolDTO, True- show navigation property(SchoolId)

            return Schools;                     //<School, SchoolDTO>(true) - doesn't work, saying invalid column SchoolId (true enables navigation properties- SchoolId to be shown)
        }


        [HttpGet("{id:int}")]

        public async Task<ActionResult<SchoolDTO>> Get(int id, bool include = false)
        {
            try
            {
                var dto = await _context.SingleAsync<School, SchoolDTO>(s => s.Id.Equals(id), include); // Retrieve SchoolDto with an Id
                if (dto == null) return NotFound(); //If was null then show NotFound
                return dto; //Otherwise return the dto
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure"); //If an error occurred in our server and had nothing to do with the user, then show internal server error 
            }
        }

        [HttpPost]

        public async Task<ActionResult<SchoolDTO>> Post(SchoolDTO model) //Creating a School through API- using a DTO and a model instance of it
        {
            try
            {
                if (model == null) return BadRequest("No entity provided"); //If the user hasn't provided any data then BadRequest

                var id = await _context.CreateAsync<SchoolDTO, School>(model); //Then we need to check that id generated is not smaller than 1
                if (id < 1) return BadRequest("Unable to add the entity.");

                var dto = await _context.SingleAsync<School, SchoolDTO>(s => s.Id.Equals(id), true); //Then we try to find what has been created with SingleAsync
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

        public async Task<ActionResult<SchoolDTO>> Put(int id, SchoolDTO model) //Creating a School through API- using a DTO and a model instance of it
        {
            try
            {
                if (model == null) return BadRequest("No entity provided"); //If the user hasn't provided any data then BadRequest
                if (!id.Equals(model.Id)) return BadRequest("Differing Ids.");
                if (await _context.UpdateAsync<SchoolDTO, School>(model)) return NoContent(); //NoContent- nothing to show, but succeeded in updating the entity
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the entity"); //And if there are errors that the user is not responsible for then we throw an internal server error due to it occurring in our server 
            }

            return BadRequest("Unable to update the entity.");
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id) //Creating a School through API- using a DTO and a model instance of it
        {
            try
            {
                var exists = await _context.AnyAsync<School>(a => a.Id.Equals(id)); //If the user hasn't provided any data then BadRequest
                if (!exists) return BadRequest("Could not find related entity.");

                if (await _context.DeleteAsync<School>(d => d.Id.Equals(id))) return NoContent(); //NoContent- nothing to show, but succeeded in updating the entity
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the entity"); //And if there are errors that the user is not responsible for then we throw an internal server error due to it occurring in our server 
            }

            return BadRequest("Failed to delete the entity.");
        }

        /*    // GET: api/Schools/5
            [HttpGet("{id}")]
            public async Task<ActionResult<School>> GetSchool(int id)
            {
                var School = await _context.Schools.FindAsync(id);

                if (School == null)
                {
                    return NotFound();
                }

                return School;
            }

            // PUT: api/Schools/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutSchool(int id, School School)
            {
                if (id != School.Id)
                {
                    return BadRequest();
                }

                _context.Entry(School).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(id))
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

            // POST: api/Schools
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<School>> PostSchool(School School)
            {
                _context.Schools.Add(School);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSchool", new { id = School.Id }, School);
            }

            // DELETE: api/Schools/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteSchool(int id)
            {
                var School = await _context.Schools.FindAsync(id);
                if (School == null)
                {
                    return NotFound();
                }

                _context.Schools.Remove(School);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool SchoolExists(int id)
            {
                return _context.Schools.Any(e => e.Id == id);
            } */
    }
}


/* 
Istället för att en IMapper och VODContext kan vi kan vi ha CRUD services 
och universella CRUD operationer för varje entitet
private readonly VODContext _context;
private readonly IMapper _mapper; 

public SchoolsController(VODContext context, IMapper mapper)
{
    _context = context; //Accessing the database context through _context box/variable
    _mapper = mapper; //USing auto mapper through _mapper box
}
*/