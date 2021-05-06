using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NetCore.Services.JobQueue
{
    public interface IJobService
    {
        Task<Training.Task.Api.Models.Task> GetJobByID(int jobID);
        Task<List<Training.Task.Api.Models.Task>> GetJobs();
    }
}
