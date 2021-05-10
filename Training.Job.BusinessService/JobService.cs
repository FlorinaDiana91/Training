using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Job.BusinessService.ExtensionMethods;
using Training.Job.DAL;
using Training.Job.DataContracts;

namespace Training.Job.BusinessService
{
    public class JobService:IJobService
    {
        private readonly Training.Job.DAL.Interfaces.IJobRepository _jobRepository;

        public JobService(DAL.Interfaces.IJobRepository jobRepository)
        {
            this._jobRepository = jobRepository;
        }

        public async Task<bool> AddTaskResource(DAL.DataModel.Task task)
        {
           return await _jobRepository.AddTaskResource(task);
        }

        public async Task<bool> DeleteTaskResource(int id)
        {
            return await _jobRepository.DeleteTaskResource(id);
        }
        public async Task<TaskResource> GetJobByID(int id)
        {
            var job = await _jobRepository.GetJobByID(id);
            if (job != null)
            {
                return job.MapModelToDto();
            }

            return null;
        }

        public async Task<List<TaskResource>> GetJobs()
        {
            var jobs = await _jobRepository.GetJobs();
            if (jobs != null)
            {
                return jobs.MapListModelToDtoList();
            }

            return null;
        }

        public async Task<bool> UpdateTaskResource(TaskResource task)
        {
            return await _jobRepository.UpdateTaskResource(task);
        }
    }
}
