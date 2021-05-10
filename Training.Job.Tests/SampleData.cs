using System;
using Training.Job.DAL.DataModel;

namespace Training.Job.Tests
{
    public static class SampleData
    {
        public static Task[] TaskQueueItems =
        {
            new Task() { TaskID = 100, TaskDate = DateTime.Now, TaskName = "Job1a", TaskProcessed = false, TypeID = 1  },
            new Task() { TaskID = 101, TaskDate = DateTime.Now, TaskName = "Job2", TaskProcessed = false, TypeID = 2  },
            new Task() { TaskID = 102, TaskDate = DateTime.Now, TaskName = "Trigger", TaskProcessed = false, TypeID = 3  },
            new Task() { TaskID = 103, TaskDate = DateTime.Now, TaskName = "Notification", TaskProcessed = false, TypeID = 4  },
            new Task() { TaskID = 104, TaskDate = DateTime.Now, TaskName = "Send message", TaskProcessed = false, TypeID = 5  },
            new Task() { TaskID = 105, TaskDate = DateTime.Now, TaskName = "Mail", TaskProcessed = false, TypeID = 2  },
            new Task() { TaskID = 106, TaskDate = DateTime.Now, TaskName = "Job3", TaskProcessed = false, TypeID = 2  },
            new Task() { TaskID = 107, TaskDate = DateTime.Now, TaskName = "Job4", TaskProcessed = false, TypeID = 2  },
            new Task() { TaskID = 108, TaskDate = DateTime.Now, TaskName = "Job5", TaskProcessed = false, TypeID = 2  },
        };
    }
}
