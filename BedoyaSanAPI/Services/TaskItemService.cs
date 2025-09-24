using BedoyaSanAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BedoyaSanAPI.Services
{
    public class TaskItemService
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskItemService(IOptions<TasksDBSettings> tasksDBSettings)
        {
            var mongoClient = new MongoClient(
            tasksDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tasksDBSettings.Value.DatabaseName);

            _tasks = mongoDatabase.GetCollection<TaskItem>(
                tasksDBSettings.Value.TasksCollectionName);
        }

        public async Task<List<TaskItem>> GetAsync() =>
        await _tasks.Find(_ => true).ToListAsync();

        public async Task<TaskItem?> GetAsync(string id) =>
            await _tasks.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TaskItem newTask) =>
            await _tasks.InsertOneAsync(newTask);

        public async Task UpdateAsync(string id, TaskItem updatedTask) =>
            await _tasks.ReplaceOneAsync(x => x.Id == id, updatedTask);

        public async Task RemoveAsync(string id) =>
            await _tasks.DeleteOneAsync(x => x.Id == id);
    }
}
