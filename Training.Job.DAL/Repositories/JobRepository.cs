using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Training.Job.DAL.DataModel;
using Training.Job.DAL.Interfaces;
using Training.Job.DataContracts;

namespace Training.Job.DAL
{
    public class JobRepository : IJobRepository
    {
        //il sterg
        protected readonly TrainingEntities _context;

        public JobRepository(TrainingEntities context)
        {
            //il sterg
            _context = context;
        }

        public async Task<DataModel.Task> GetJobByID(int jobID)
        {
            try
            {
                var job = await _context.Tasks.Include(f => f.TaskType).FirstOrDefaultAsync(f => f.TaskID == jobID);
                return job;

            }
            catch (System.Exception ex)
            {
                throw;
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

        public async Task<bool> DeleteTaskResource(int id)
        {
            var message = false;
            try
            {
                var job = await GetJobByID(id);
                if (job != null)
                {
                    _context.Tasks.Remove(job);
                    await _context.SaveChangesAsync();
                    message = true;
                }

            }
            catch (System.Exception ex)
            {
                throw;
            }

            return message;
        }

        public async Task<bool> UpdateTaskResource(TaskResource task)
        {
            var job = await GetJobByID(task.TaskID);
            if (job != null)
            {
                job.TaskProcessed = task.TaskProcessed;
                job.TaskDate = task.TaskDate;
                job.TaskName = task.TaskName;

                _context.Entry(job).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> AddTaskResource(DataModel.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}