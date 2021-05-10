using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Job.DataContracts;

namespace Training.Job.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task<DataModel.Task> GetJobByID(int jobID);
        Task<List<DataModel.Task>> GetJobs();
        Task<bool> DeleteTaskResource(int id);
        Task<bool> UpdateTaskResource(TaskResource task);
        Task<bool> AddTaskResource(DataModel.Task task);
    }
}
