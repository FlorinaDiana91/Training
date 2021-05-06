using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NetCore.Services.JobQueue
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            this._jobRepository = jobRepository;
        }

        public async Task<Training.Task.Api.Models.Task> GetJobByID(int jobID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Training.Task.Api.Models.Task>> GetJobs()
        {
            return await _jobRepository.ListAsync();
        }
    }
}
