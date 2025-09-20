using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebWorker.Data.Entities;
using WebWorker.Data.Entities.Identity;
using WebWorker.Interfaces;
using WebWorker.Models.Seeder;

namespace WebWorker.Data;

public static class WorkerDbSeeder
{
    public static async Task SeedData(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppWorkerDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        // Ensure the database is created and migrations are applied
        await dbContext.Database.MigrateAsync();

        // Seed roles
        foreach (var role in Constants.Roles.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new RoleEntity(role));
            }
        }
        // Seed default admin user
        if (!dbContext.Users.Any())
        {
            var adminUser = new UserEntity
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Юхим",
                LastName = "Манько"
            };
            var result = await userManager.CreateAsync(adminUser, "123456");
            if (result.Succeeded)
            {
                // Assign the admin role to the user
                await userManager.AddToRoleAsync(adminUser, Constants.Roles.Admin);
            }
            else
            {
                throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var user = new UserEntity
            {
                UserName = "user@ukr.net",
                Email = "user@ukr.net",
                FirstName = "Петро",
                LastName = "Підкаблучник"
            };
            var userResult = await userManager.CreateAsync(user, "123456");
            if (userResult.Succeeded)
            {
                // Assign the user role to the user
                await userManager.AddToRoleAsync(user, Constants.Roles.User);
            }
            else
            {
                throw new Exception($"Failed to create user: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
            }
        }

        if (!dbContext.Ingredients.Any())
        {
            var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
            var jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "JsonData", "Ingredients.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var items = JsonSerializer.Deserialize<List<SeederIngredientModel>>(jsonData);
                    var entityItems = mapper.Map<List<IngredientEntity>>(items);
                    foreach (var entity in entityItems)
                    {
                        entity.Image =
                            await imageService.SaveImageFromUrlAsync(entity.Image);
                    }

                    await dbContext.Ingredients.AddRangeAsync(entityItems);
                    await dbContext.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Json Parse Data {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Not Found File Ingredients.json");
            }
        }

    }


}
