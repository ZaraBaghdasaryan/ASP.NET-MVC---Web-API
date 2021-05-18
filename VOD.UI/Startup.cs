using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Services;
using VOD.UI.Services;
using VOD.Common.DTOModels;
using System.Net.Http;
using VOD.Common.Services;

namespace VOD.UI
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
            services.AddDefaultIdentity<VODUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<VODContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IDbReadService, DbReadService>();
            services.AddScoped<IUIReadService, UIReadService>();
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
                    src => src.MapFrom(s => s.ImageUrl));
                
                cfg.CreateMap<Module, ModuleDTO>()
                .ForMember(dest => dest.ModuleTitle,
                    src => src.MapFrom(s => s.Title));


            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient("AdminClient", client =>
            {
                client.BaseAddress = new System.Uri("http://localhost:5500"); //Api server address
                client.Timeout = new System.TimeSpan(0, 0, 30); //If it doesn't manage to connect in 30 sec then receive an error message
                client.DefaultRequestHeaders.Clear(); //Emptying the default headers so we can add our own
            }).ConfigurePrimaryHttpMessageHandler(handler =>
            new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip //Storing the dtos zipped with less bytes to save space by using the HttpClientHandler
            });

            services.AddScoped<IHttpClientFactoryService, HttpClientFactoryService>();
            services.AddScoped<IApiService, ApiService>();
                
                

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VODContext db)
        {
            db.SeedMembershipData();

            db.SeedAdminData();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}