using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NetCore.Services.JobQueue
{
    public interface IJobRepository
    {
        //Task<Task> GetJobByID(int jobID);
        Task<List<Training.Task.Api.Models.Task>> ListAsync();
    }
}
