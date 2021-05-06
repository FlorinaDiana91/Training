using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NetCore.Services.JobQueue.Models
{
    public partial class TaskType
    {
        public TaskType()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
