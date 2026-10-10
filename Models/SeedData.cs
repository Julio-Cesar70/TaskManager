using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TaskManager.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new TaskManagerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<TaskManagerContext>>()))
        {
            // Look for any tasks.
            if (context.Task.Any())
            {
                return;   // DB has been seeded
            }
            context.Task.AddRange(
                new Task
                {
                    Name = "Cozinhar",
                    Description = "Preparar o almoço para a família"
                },
                new Task
                {
                    Name = "Estudar",
                    Description = "Estudar para a prova de matemática"
                },
                new Task
                {
                    Name = "Limpar a casa",
                    Description = "Lavar a louça e passar a vassoura"
                },
                new Task
                {
                    Name = "Assistir a um filme",
                    Description = "Assistir ao novo filme do Homem-Aranha"
                }
            );
            context.SaveChanges();
        }
    }
}