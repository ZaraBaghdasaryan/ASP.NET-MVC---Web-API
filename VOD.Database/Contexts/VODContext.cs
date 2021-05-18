using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VOD.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace VOD.Database.Contexts
{
    /* VODContext class is the bridge between the database and the application.*/
    public class VODContext : IdentityDbContext<VODUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<School> Schools { get; set; }

        private void SeedData(ModelBuilder builder) 
        {
            #region Admin Credentials Properties
            var email = "a@b.c";
            var password = "Test123_";

            var user = new VODUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                EmailConfirmed = true

            };

            var passwordHasher = new PasswordHasher<VODUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            // Add user to database
            builder.Entity<VODUser>().HasData(user);
            #endregion 

            /*Adding the new user to the database. */

            #region Admin Roles and Claims
            var admin = "Admin";
            var role = new IdentityRole { Id = "1", Name = admin, NormalizedName = admin.ToUpper() };

            /*Adding the role to the AspNetRoles table.*/

            builder.Entity<IdentityRole>().HasData(role); 

            /*Assigning the role to the Admin user */

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });

            /*Adding user claims to Admin and VODUser*/

            builder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string> { Id = 1, ClaimType = admin, ClaimValue = "true", UserId = user.Id }); 

            builder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string> { Id = 2, ClaimType = "VODUser", ClaimValue = "true", UserId = user.Id });
            #endregion
        }

        public void SeedAdminData() /*The entire code is run only when we create the database and run the application the first time */
        {
            #region Admin Credentials Properties
            var adminEmail = "a@b.c";
            var adminPassword = "Test123__";
            var adminUserId = string.Empty;

            // Fetch the Admin User Id
            if (Users.Any(u => u.Email.Equals(adminEmail))) adminUserId = (Users.SingleOrDefault(u => u.Email.Equals(adminEmail))).Id;

            else
            {
                var user = new VODUser {
                    Email = adminEmail,
                    UserName = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    NormalizedUserName = adminEmail.ToUpper()
                };

                var passwordHasher = new PasswordHasher<VODUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, adminPassword);

                Users.Add(user);
                SaveChanges();
                adminUserId = (Users.SingleOrDefault(u => u.Email.Equals(adminEmail))).Id;

            }
            #endregion

            #region Add Admin Role
            var adminRoleName = "Admin";

            /*Fetching a role with the name Admin from the table and storing it in a variable named adminRole. */

            var adminRole = Roles.SingleOrDefault(r => r.Name.ToLower().Equals(adminRoleName.ToLower()));

            /*Checking if Admin was successfully fetched, default value means that the role wasn’t in the table */

            if (adminRole == default) {

                /*Add a new role to the table with the Name and Id 1. */   /*Creating it in the database table */
                Roles.Add(new IdentityRole()
                {

                    Name = adminRoleName,
                    NormalizedName = adminRoleName.ToUpper(),
                    Id = "1"

                });

                /*Saving the changes to the database and fetching the role into the adminRole variable */   /*Bringing it to the application? */
                SaveChanges(); adminRole = Roles.SingleOrDefault(r => r.Name.ToLower().Equals(adminRoleName.ToLower()));
            }
            #endregion
            /*Adding the Admin role to the admin user */

            #region Seed Data for the Admin
            if (adminUserId != string.Empty) /*Alternative - if (!adminUserId.Equals(string.Empty)) */

            {
                /*Checking that adminRole exists. */ /*Default means not existing (default data fed, not human made) */
                if (adminRole != default)
                {
                    /*Fetching role from the UserRoles table.*/

                    var userRoleExists = UserRoles.Any(ur => ur.RoleId.Equals(adminRole.Id) && ur.UserId.Equals(adminUserId));

                    if (!userRoleExists)
                        /*If missing then adding the Admin role to the user */
                        UserRoles.Add(new IdentityUserRole<string> { RoleId = adminRole.Id, UserId = adminUserId });

                }
                #endregion
                #region Add User Claims
                var claimType = "Admin"; /*Claim to have the role of an admin */
                /*Checking if there is claim associated with adminUserId */
                var userClaimExists = UserClaims.Any(uc => uc.ClaimType.ToLower().Equals(claimType.ToLower()) && uc.UserId.Equals(adminUserId));

                if (!userClaimExists) UserClaims.Add(new IdentityUserClaim<string>

                {
                    ClaimType = claimType,
                    ClaimValue = "true",
                    UserId = adminUserId

                });

                claimType = "VODUser"; /*Claim to have the role of a regular user */
                userClaimExists = UserClaims.Any(uc => uc.ClaimType.ToLower().Equals(claimType.ToLower()) && uc.UserId.Equals(adminUserId));

                if (!userClaimExists)

                    UserClaims.Add(new IdentityUserClaim<string> /*The entire code is run only when we create the database and run the application the first time */
                    {
                        ClaimType = claimType,
                        ClaimValue = "true",
                        UserId = adminUserId

                    });
                #endregion
            }
            SaveChanges();

        }

        public void SeedMembershipData()
        {
            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

            var email = "a@b.c";
            var userId = string.Empty;


            if (Users.Any(r => r.Email.Equals(email)))
                userId = Users.First(r => r.Email.Equals(email)).Id;
            else
            
                return;

            if (!Instructors.Any())
            {
                var instructors = new List<Instructor>
                {
                new Instructor {
                Name = "John Doe",
                Description = description.Substring(20, 50),
                Thumbnail = "/images/avatars/Ice-Age-Scrat-icon.png"},
                new Instructor {
                Name = "Jane Doe",
                Description = description.Substring(30, 40),
                Thumbnail = "/images/avatars/Ice-Age-Scrat-icon.png"

                }
                };
                Instructors.AddRange(instructors);
                SaveChanges();
            }
            if (Instructors.Count() < 2) return;

            if (!Courses.Any())
            {
                var instructorId1 = Instructors.First().Id;
                var instructorId2 = Instructors.Skip(1).FirstOrDefault().Id;
                var courses = new List<Course>
            {
                new Course {
                InstructorId = instructorId1,
                Title = "Course 1",
                Description = description,
                ImageUrl = "/images/CourseImages/course1.jpg",
                MarqueeImageUrl = "/images/CourseImages/laptop.jpg"
                },
                new Course {
                InstructorId = instructorId2, 
                Title = "Course 2",
                Description = description,
                ImageUrl = "/images/CourseImages/course2.jpg",
                MarqueeImageUrl = "/images/CourseImages/laptop.jpg"
                },
                new Course {
                InstructorId = instructorId1,
                Title = "Course 3",
                Description = description,
                ImageUrl = "/images/CourseImages/course3.jpg",
                MarqueeImageUrl = "/images/CourseImages/laptop.jpg"
                }
                };
                Courses.AddRange(courses);
                SaveChanges();
            }

            if (Courses.Count() < 3) return;

            var courseId1 = Courses.First().Id;
            var courseId2 = Courses.Skip(1).FirstOrDefault().Id;
            var courseId3 = Courses.Skip(2).FirstOrDefault().Id; 

            if (!UserCourses.Any())
            {
                if (!courseId1.Equals(int.MinValue))
                    UserCourses.Add(new UserCourse
                    { UserId = userId, CourseId = courseId1 });
                if (!courseId2.Equals(int.MinValue))
                    UserCourses.Add(new UserCourse
                    { UserId = userId, CourseId = courseId2 }); 
                if (!courseId3.Equals(int.MinValue))
                    UserCourses.Add(new UserCourse
                    { UserId = userId, CourseId = courseId3 });
                SaveChanges();

                if (UserCourses.Count() < 3) return;
            }
            if (!Modules.Any())
                {
                var modules = new List<Module>
                {
                new Module { Course = Find<Course>(courseId1),
                Title = "Module 1" },
                new Module { Course = Find<Course>(courseId1),
                Title = "Module 2" },
                new Module { Course = Find<Course>(courseId2),
                Title = "Module 3" }
                };
                Modules.AddRange(modules);
                SaveChanges();
                }
            if (Modules.Count() < 3) return;

            var moduleId1 = Modules.First().Id;
            var moduleId2 = Modules.Skip(1).FirstOrDefault().Id;
            var moduleId3 = Modules.Skip(2).FirstOrDefault().Id;

            if (!Videos.Any())
            {
                var videos = new List<Video>
                {
                new Video { ModuleId = moduleId1, CourseId = courseId1,
                Title = "Video 1 Title",
                Description = description.Substring(1, 35),
                Duration = 50, Thumbnail = "/images/CourseImages/video1.jpg",
                Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                },
                new Video { ModuleId = moduleId1, CourseId = courseId1,
                Title = "Video 2 Title",
                Description = description.Substring(5, 35),
                Duration = 45, Thumbnail = "/images/CourseImages/video2.jpg",
                Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                },
                new Video { ModuleId = moduleId1, CourseId = courseId1,
                Title = "Video 3 Title",
                Description = description.Substring(10, 35),
                Duration = 41, Thumbnail = "/images/CourseImages/video3.jpg",
                Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                },
                new Video { ModuleId = moduleId3, CourseId = courseId2,
                Title = "Video 4 Title",
                Description = description.Substring(15, 35),
                Duration = 41, Thumbnail = "/images/CourseImages/video4.jpg",
                Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                },
                new Video { ModuleId = moduleId2, CourseId = courseId1,
                Title = "Video 5 Title",
                Description = description.Substring(20, 35),
                Duration = 42, Thumbnail = "/images/CourseImages/video5.jpg",
                Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY" 
                }
                };
                Videos.AddRange(videos);
                SaveChanges();
               }
            if (!Downloads.Any())
            {
                var downloads = new List<Download>
            {
                new Download{ModuleId = moduleId1, CourseId = courseId1,
                Title = "ADO.NET 1 (PDF)", Url = "https://some-url" },
                new Download{ModuleId = moduleId1, CourseId = courseId1,
                Title = "ADO.NET 2 (PDF)", Url = "https://some-url" },
                new Download{ModuleId = moduleId3, CourseId = courseId2,
                Title = "ADO.NET 1 (PDF)", Url = "https://some-url" }
                };
                Downloads.AddRange(downloads);
                SaveChanges();
                }
            }




        /*Adding a constructor with a DbContextOptions<VODContext> parameter named options */
        public VODContext(DbContextOptions<VODContext> options): base(options)

        {

        }

        /*Method  OnModelCreating seeds data in the database when it is created.*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); /*Call the same method on the IdentityDbContext base class.*/
            SeedData(builder);
            
            builder.Entity<UserCourse>().HasKey(uc => new { uc.UserId, uc.CourseId }); //Composite key
            
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; //cascading delete will delete all related records to the one being deleted;
                                                                       //for instance order- all order rows
            }
        }
    }

   
}



/*To do- Call the SeedData method at the end of the OnModelCreating method and pass in its builder object*/