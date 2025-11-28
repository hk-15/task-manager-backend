using backend.Controllers;
using backend.Models.Response;
using backend.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using backend.Exceptions;
using backend.Models.Request;

namespace Backend.Tests.Controllers;

public class TasksControllerTests
{
    private readonly ITasksService _tasksService;
    private readonly TasksController _controller;
    public TasksControllerTests()
    {
        _tasksService = A.Fake<ITasksService>();
        _controller = new TasksController(_tasksService);
    }

    [Fact]
    public async Task GetTaskById_ShouldReturnOk_WhenTaskExists()
    {
        // Arrange
        int taskId = 1;
        var expectedResponse = new CaseworkerTaskResponse
        {
            Id = taskId,
            Title = "Test Task",
            Description = "This is a test task",
            Status = "Open",
            DueTime = DateTime.UtcNow.AddDays(1).ToString()
        };

        A.CallTo(() => _tasksService.GetById(taskId)).Returns(Task.FromResult(expectedResponse));

        // Act
        var result = await _controller.GetTaskById(taskId);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        var actualResponse = okResult.Value as CaseworkerTaskResponse;
        actualResponse.Should().NotBeNull();
        actualResponse.Id.Should().Be(expectedResponse.Id);
    }

    [Fact]
    public async Task GetTaskById_ShouldReturn404_WhenExceptionThrown()
    {
        //Arrange
        int taskId = 999;
        string errorMessage = "Task not found";
        A.CallTo(() => _tasksService.GetById(taskId)).ThrowsAsync(new Exception("Task not found"));

        //Act
        var result = await _controller.GetTaskById(taskId);

        //Assert
        var notFound = (NotFoundObjectResult)result.Result!;
        notFound.StatusCode.Should().Be(404);
        var error = (ErrorResponse)notFound.Value!;

        error.Message.Should().Be(errorMessage);
    }

    [Fact]
    public async Task AddTask_ShouldReturnOk_WhenModelIsValid()
    {
        //Arrange
        var request = new CaseworkerTaskRequest
        {
            Title = "New Task",
            Description = "test task",
            Status = "Not started",
            DueTime = new DateTime(),
        };

        int newTaskId = 1;
        var newTaskResponse = new CaseworkerTaskResponse
        {
            Id = newTaskId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            DueTime = request.DueTime.ToString(),
        };

        A.CallTo(() => _tasksService.Add(request)).Returns(Task.FromResult(newTaskId));
        A.CallTo(() => _tasksService.GetById(newTaskId)).Returns(Task.FromResult(newTaskResponse));


        //Act
        var result = await _controller.AddTask(request);

        //Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        var actualResponse = okResult.Value as CaseworkerTaskResponse;
        actualResponse.Should().NotBeNull();
        actualResponse.Id.Should().Be(newTaskId);
    }

}