using EjercicioPromart.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EjercicioPromart.Entitys
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any board games.
                if (context.Employee.Any())
                {
                    return;   // Data was already seeded
                }

                context.Employee.AddRange(
                    new Employee
                    {
                        id = 1,
                        employee_name = "Tiger Nixon",
                        employee_salary = 320800,
                        employee_age = 61,
                        profile_image = "asdasd"
                    });

                context.SaveChanges();
            }
        }
    }
}
