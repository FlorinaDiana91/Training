using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Job.DataContracts;

namespace Training.Job.BusinessService
{
    public interface IJobService
    {
        Task<TaskResource> GetJobByID(int id);
        Task<List<TaskResource>> GetJobs();
    }
}