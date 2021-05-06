using System.Collections.Generic;
using Training.Job.DAL.DataModel;
using Training.Job.DataContracts;

namespace Training.Job.BusinessService.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static TaskResource MapModelToDto(this Task task)
        {
            if (task == null)
            {
                return null;
            }

            return new TaskResource()
            {
                TaskID = task.TaskID,
                TaskName = task.TaskName,
                TaskProcessed = task.TaskProcessed,
                TypeName = task.TaskType.Name,
                TaskDate = task.TaskDate
            };
        }

        public static List<TaskResource> MapListModelToDtoList(this List<Task> tasks)
        {
            var taskResources = new List<DataContracts.TaskResource>();
            foreach (var a in tasks)
            {
                taskResources.Add(new TaskResource()
                {
                    TaskID = a.TaskID,
                    TaskName = a.TaskName,
                    TaskProcessed = a.TaskProcessed,
                    TypeName = a.TaskType != null ? a.TaskType.Name : string.Empty,
                    TaskDate = a.TaskDate
                });
            }
            return taskResources;
        }

    }
}
