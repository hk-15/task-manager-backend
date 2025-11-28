using backend.Exceptions;
using backend.Models.Request;
using backend.Models.Response;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("/tasks")]
public class TasksController(ITasksService tasksService) : ControllerBase
{
    private readonly ITasksService _tasksService = tasksService;

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CaseworkerTaskResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CaseworkerTaskResponse>> GetTaskById(int id)
    {
        try
        {
            var response = await _tasksService.GetById(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(new ErrorResponse { Message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(CaseworkerTaskResponse))]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CaseworkerTaskResponse>> AddTask([FromBody] CaseworkerTaskRequest newTask)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            int newTaskId = await _tasksService.Add(newTask);
            return await GetTaskById(newTaskId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}