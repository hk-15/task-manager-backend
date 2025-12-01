using System.Globalization;
using backend.Models.Database;
using backend.Repositories;
using backend.Services;
using FakeItEasy;
using FluentAssertions;

namespace Backend.Tests.Services;
public class TasksServiceTests
{
    private readonly ITasksRepo _tasksRepo;
    private readonly ITasksService _tasksService;

    public TasksServiceTests()
    {
        _tasksRepo = A.Fake<ITasksRepo>();
        _tasksService = new TasksService(_tasksRepo);
    }

    [Fact]
    public async Task GetById_ShouldReturnTask_WhenTaskExists()
    {
        //Arrange
        int taskId = 1;
        var dbTask = new CaseworkerTask
        {
            Id = taskId,
            Title = "Test Task",
            Description = "This is a test task",
            Status = "Open",
            DueTime = DateTime.UtcNow.AddDays(1)
        };
        A.CallTo(() => _tasksRepo.GetById(taskId)).Returns(Task.FromResult<CaseworkerTask?>(dbTask));

        //Act
        var result = await _tasksService.GetById(taskId);

        //Asert
        result.Should().NotBeNull();
        result.Id.Should().Be(dbTask.Id);
        result.Title.Should().Be(dbTask.Title);
        result.Description.Should().Be(dbTask.Description);
        result.Status.Should().Be(dbTask.Status);
        result.DueTime.Should().Be(dbTask.DueTime.ToString(new CultureInfo("en-GB")));
    }

    [Fact]
     public async Task GetById_ShouldThrowException_WhenTaskIsNull()
    {
        //Arrange
        int taskId = 99;
        A.CallTo(() => _tasksRepo.GetById(taskId)).Returns(Task.FromResult<CaseworkerTask?>(null));
        
        //Act
        Func<Task> act = async () => await _tasksService.GetById(taskId);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage($"Task with id {taskId} not found");

    }
}