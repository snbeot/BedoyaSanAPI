using BedoyaSanAPI.Models;
using BedoyaSanAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BedoyaSanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskItemService _tasksService;

        public TasksController(TaskItemService taskService) =>
            _tasksService = taskService;

        [HttpGet]
        public async Task<List<TaskItem>> Get() =>
            await _tasksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TaskItem>> Get(string id)
        {
            var taskItem = await _tasksService.GetAsync(id);

            if (taskItem is null)
            {
                return NotFound();
            }

            return taskItem;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaskItem newTask)
        {
            await _tasksService.CreateAsync(newTask);

            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TaskItem updatedTask)
        {
            var taskItem = await _tasksService.GetAsync(id);

            if (taskItem is null)
            {
                return NotFound();
            }

            updatedTask.Id = taskItem.Id;

            await _tasksService.UpdateAsync(id, updatedTask);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var taskItem = await _tasksService.GetAsync(id);

            if (taskItem is null)
            {
                return NotFound();
            }

            await _tasksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
