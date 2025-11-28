using backend.Models.Database;
using backend.Models.Request;
using backend.Models.Response;
using backend.Repositories;

namespace backend.Services;

public interface ITasksService
{
    Task<CaseworkerTaskResponse> GetById(int id);
    Task<int> Add(CaseworkerTaskRequest newTask);
}

public class TasksService(ITasksRepo tasksRepo) : ITasksService
{
    private readonly ITasksRepo _tasksRepo = tasksRepo;

    public async Task<CaseworkerTaskResponse> GetById(int id)
    {
        var task = await _tasksRepo.GetById(id) ?? throw new Exception($"Task with id {id} not found");
        return new CaseworkerTaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            DueTime = task.DueTime.ToString()
        };
    }

    public async Task<int> Add(CaseworkerTaskRequest newTask)
    {
        CleanData(newTask);
        CaseworkerTask task = new()
        {
            Title = newTask.Title,
            Description = newTask.Description,
            Status = newTask.Status,
            DueTime = newTask.DueTime,
        };
        return await _tasksRepo.Add(task);
    }

    private static void CleanData(CaseworkerTaskRequest request)
    {
        request.Title = request.Title.Trim();
        if (!string.IsNullOrEmpty(request.Description)) request.Description = request.Description.Trim();
        request.Status = request.Status.Trim();
    }
}