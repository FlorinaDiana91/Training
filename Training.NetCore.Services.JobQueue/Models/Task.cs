namespace Training.NetCore.Services.JobQueue.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public bool TaskProcessed { get; set; }
        public int? TypeId { get; set; }

        public virtual TaskType Type { get; set; }
    }
}
