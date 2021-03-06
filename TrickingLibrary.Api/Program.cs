using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

namespace TrickingLibrary.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var testUser = new IdentityUser("test"){Email = "test@test.com"};
                    userMgr.CreateAsync(testUser, "password").GetAwaiter().GetResult();

                    var mod = new IdentityUser("mod"){Email = "mod@test.com"};
                    userMgr.CreateAsync(mod, "password").GetAwaiter().GetResult();
                    userMgr.AddClaimAsync(mod,
                            new Claim(TrickingLibraryConstants.Claims.Role,
                                TrickingLibraryConstants.Roles.Mod))
                        .GetAwaiter()
                        .GetResult();

                    ctx.Add(new Difficulty {Id = 1, Slug = "easy", Version = 1, Active = true, Name = "Easy", Description = "Easy Test"});
                    ctx.Add(new Difficulty {Id = 2, Slug = "medium", Version = 1, Active = true, Name = "Medium", Description = "Medium Test"});
                    ctx.Add(new Difficulty {Id = 3, Slug = "hard", Version = 1, Active = true, Name = "Hard", Description = "Hard Test"});
                    ctx.Add(new Category {Id = 1, Slug = "kick", Version = 1, Active = true, Name = "Kick", Description = "Kick Test"});
                    ctx.Add(new Category {Id = 2, Slug = "flip", Version = 1, Active = true, Name = "Flip", Description = "Flip Test"});
                    ctx.Add(new Category {Id = 3, Slug = "transition", Version = 1, Active = true, Name = "Transition", Description = "Transition Test"});
                    ctx.Add(new Trick
                    {
                        Id = 1,
                        Slug = "backwards-roll",
                        Active = true,
                        Version = 1,
                        Name = "Backwards Roll",
                        Description = "This is a test backwards roll",
                        Difficulty = "easy",
                        TrickCategories = new List<TrickCategory> {new TrickCategory {CategoryId = 2}}
                    });
                    ctx.Add(new Trick
                    {
                        Id = 2,
                        Slug = "forwards-roll",
                        Active = true,
                        Version = 1,
                        Name = "Forwards Roll",
                        Description = "This is a test forwards roll",
                        Difficulty = "easy",
                        TrickCategories = new List<TrickCategory> {new TrickCategory {CategoryId = 2}}
                    });
                    ctx.Add(new Trick
                    {
                        Id = 3,
                        Slug = "back-flip",
                        Active = true,
                        Version = 1,
                        Name = "Back Flip",
                        Description = "This is a test back flip",
                        Difficulty = "medium",
                        TrickCategories = new List<TrickCategory> {new TrickCategory {CategoryId = 2}},
                        Prerequisites = new List<TrickRelationship>
                        {
                            new TrickRelationship {PrerequisiteId = 1}
                        }
                    });
                    ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "Test description, I've tried to go for max height",
                        Video = new Video
                        {
                            VideoLink = "https://localhost:5001/api/files/video/one.mp4",
                            ThumbLink = "https://localhost:5001/api/files/image/one.jpg"
                        },
                        VideoProcessed = true,
                        UserId = testUser.Id,
                    });
                    ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "Test description, I've tried to go for min height",
                        Video = new Video
                        {
                            VideoLink = "https://localhost:5001/api/files/video/two.mp4",
                            ThumbLink = "https://localhost:5001/api/files/image/two.jpg"
                        },
                        VideoProcessed = true,
                        UserId = testUser.Id,
                    });
                    ctx.Add(new ModerationItem
                    {
                        Target = 3,
                        Type = ModerationTypes.Trick,
                    });
                    ctx.SaveChanges();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}