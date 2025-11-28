using backend.Database;
using backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public interface ITasksRepo
{
    Task<CaseworkerTask?> GetById(int id);
    Task<int> Add(CaseworkerTask newTask);
}

public class TasksRepo(TaskManagerDbContext context) : ITasksRepo
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<CaseworkerTask?> GetById(int id)
    {
        return await _context.CaseworkerTasks.FirstOrDefaultAsync(task => task.Id == id);
    }

    public async Task<int> Add(CaseworkerTask newTask)
    {
        await _context.CaseworkerTasks.AddAsync(newTask);
        await _context.SaveChangesAsync();
        return newTask.Id;
    }
}