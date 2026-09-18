using Microsoft.EntityFrameworkCore;

public class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<TaskManager.Models.Task> Task { get; set; } = default!;
}
