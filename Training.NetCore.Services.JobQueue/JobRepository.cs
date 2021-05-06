using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.NetCore.Services.JobQueue
{
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(TrainingContext context) : base(context)
        {

        }

        public async Task<List<Models.Task>> ListAsync()
        {
            var f = await _context.Tasks.Include(ff => ff.Type).ToListAsync();
            return f;
        }
    }
}
