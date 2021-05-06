namespace Training.NetCore.Services.JobQueue.Dto
{
    public class TaskResource
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public bool TaskProcessed { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
    }
}
