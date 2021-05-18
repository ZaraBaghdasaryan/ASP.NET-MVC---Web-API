using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Interfaces;

namespace VOD.Database.Services
{
    public class CRUDService : ICRUDService
    {
        private readonly IDbReadService _dbReadService;
        private readonly VODContext _context;
        private readonly IMapper _mapper;

        public CRUDService(IDbReadService dbReadService, VODContext context, IMapper mapper)
        {
            _dbReadService = dbReadService; //Read data
            _context = context; //Manipulate data //Database representation 
            _mapper = mapper; //Convert entities to DTOs

        }  
        public async Task<List<TDto>> GetAsync<TEntity, TDto>(bool include = false)
            where TEntity : class
            where TDto : class
        {
            if (include) _dbReadService.Include<TEntity>(); //Talk to the database, tell what to include (including navigation properties
            var entities = await _dbReadService.GetAsync<TEntity>(); // If include is true (there are navigation properties) then include - ex. SchoolId in Course
            return _mapper.Map<List<TDto>>(entities); //Converting entities to Dtos and returning a list of DTOs through entities variable
        }

        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression, bool include = false)
            where TEntity : class
            where TDto : class
        {
            if (include) _dbReadService.Include<TEntity>(); //Reading it with navigation properties 
            var entity = await _dbReadService.SingleAsync(expression);
            return _mapper.Map<TDto>(entity);  //First CRUDService, then DbReadService, then Mapper, then CourseController returning the Dto (32 minutes at the 2nd lecture)
        }                                      //Sending a Dto, mapping with an entity and returning a Dto


        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _dbReadService.AnyAsync(expression);
        }

        public async Task<int> CreateAsync<TDto, TEntity>(TDto item) where TDto : class where TEntity : class
            //Before Entity to DTO, now DTO to entity since it is the Create method                                                                                          
            //Taking a DTO, saving an Entity in the database
        {
            try
            {
                var entity = _mapper.Map<TEntity>(item); //Sending a Dto while creating, converting it into an entity, then returning as a Dto again
                _context.Add(entity);                    //Generating and adding the Id to the database as a memory representation (list of operations to execute) in the entity variable
                var succeeded = await _context.SaveChangesAsync() >= 0; //Returning a Negative or Positive value, if it is bigger or equal to 0 then we have succeeded // Either -1 or 1, so we are turning the boolean expression to numbers 
                if (succeeded) return (int)entity.GetType().GetProperty("Id").GetValue(entity);
                // if succeeded manages to store the entity, we trace it first by data type, then find which entity it is (ex Course), then we check the property Id (which we decided will be the unique identifier for all entities), and finally, we find the exact value of the Id, ex. 2
                //  Id is being saved in the id variable in the CreateAsync method in the CourseController
                // (int)entity - casting the data type as int as we know that Id is always an int
            }

            catch (Exception ex)
            {
                
            }

            return -1;
        }

        public async Task<bool> UpdateAsync<TDto, TEntity>(TDto item)
            where TDto : class
            where TEntity : class
        {
            try
            {
                var entity = _mapper.Map<TEntity>(item); //Using the Automapper to take the Dto, convert it to entity and then return the Dto to the user
                _context.Update(entity); //Please, database, update our entity through the entity variable
                return await _context.SaveChangesAsync() >= 0;
            }

            catch
            {

            }

            return false;
        }

        public async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            try
            {
                var entity =  await _dbReadService.SingleAsync(expression); //SingleAsync find and read the exact entity with the Id
                _context.Remove(entity);  
                return await _context.SaveChangesAsync() >= 0;
            }

            catch
            {
                return false;
            }

            
        }

    }
} 
   
