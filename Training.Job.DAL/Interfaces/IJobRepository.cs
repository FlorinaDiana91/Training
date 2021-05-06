using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.Job.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task<DataModel.Task> GetJobByID(int jobID);
        Task<List<DataModel.Task>> GetJobs();
    }
}
