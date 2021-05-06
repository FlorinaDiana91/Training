using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Training.Job.DAL.DataModel;
using Training.Job.DAL.Interfaces;

namespace Training.Job.DAL
{
    public class JobRepository : IJobRepository
    {
        public async Task<DataModel.Task> GetJobByID(int jobID)
        {
            using (var context = new TrainingEntities())
            {
                try
                {
                    var job = await context.Tasks.Include(f => f.TaskType).FirstOrDefaultAsync(f => f.TaskID == jobID);
                    return job;

                }
                catch (System.Exception ex )
                {
                    throw;
                }
            }
        }

        public async Task<List<DataModel.Task>> GetJobs()
        {
            using (var context = new TrainingEntities())
            {
                try
                {
                    var jobs = await context.Tasks.Include(f => f.TaskType).ToListAsync();
                    return jobs;

                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
        }
    }
}