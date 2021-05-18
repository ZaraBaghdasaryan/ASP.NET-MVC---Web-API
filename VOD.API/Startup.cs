using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Interfaces;
using VOD.Database.Services;

namespace VOD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VODContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddScoped<ICRUDService, CRUDService>();
            services.AddScoped<IDbReadService, DbReadService>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Video, VideoDTO>();

                cfg.CreateMap<Instructor, InstructorDTO>()
                 .ForMember(dest => dest.InstructorName,
                     src => src.MapFrom(s => s.Name))
                .ForMember(dest => dest.InstructorDescription,
                    src => src.MapFrom(s => s.Description))
                .ForMember(dest => dest.InstructorAvatar,
                    src => src.MapFrom(s => s.Thumbnail));

                cfg.CreateMap<Download, DownloadDTO>()
                .ForMember(dest => dest.DownloadUrl,
                    src => src.MapFrom(s => s.Url))
                .ForMember(dest => dest.DownloadTitle,
                    src => src.MapFrom(s => s.Title));

                cfg.CreateMap<Course, CourseDTO>()
                .ForMember(dest => dest.CourseId, src =>
                    src.MapFrom(s => s.Id)) 
                .ForMember(dest => dest.CourseTitle,
                    src => src.MapFrom(s => s.Title))
                .ForMember(dest => dest.CourseDescription,
                    src => src.MapFrom(s => s.Description))
                .ForMember(dest => dest.MarqueeImageUrl,
                    src => src.MapFrom(s => s.MarqueeImageUrl))
                .ForMember(dest => dest.CourseImageUrl,
                    src => src.MapFrom(s => s.ImageUrl))
                .ForMember(dest => dest.School, //En sträng för att hämta bara namner inte hela entiteten 
                    src => src.MapFrom(s => s.School.Name))
                .ForMember(dest => dest.SchoolId,
                    src => src.MapFrom(s => s.School.Id))
                 .ForMember(dest => dest.Instructors,
                    src => src.MapFrom(s => s.Instructors));

                cfg.CreateMap<CourseDTO, Course>()
                .ForMember(dest => dest.ImageUrl,
                    src => src.MapFrom(s => s.CourseImageUrl)); //Why while converting from CourseDTO to Course when creating a new one, we don't add a SchoolId (cascading error, since it already exists)

                cfg.CreateMap<School, SchoolDTO>().ReverseMap();


                cfg.CreateMap<Module, ModuleDTO>()
                .ForMember(dest => dest.ModuleTitle,
                    src => src.MapFrom(s => s.Title));


            }); 

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VOD.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VOD.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
        }
    }
}

