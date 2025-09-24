namespace BedoyaSanAPI.Models
{
    public class TasksDBSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TasksCollectionName { get; set; } = null!;
    }
}
