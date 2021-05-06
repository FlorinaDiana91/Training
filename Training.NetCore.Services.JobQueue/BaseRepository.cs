using Training.NetCore.Services.JobQueue.Models;

namespace Training.NetCore.Services.JobQueue
{
    public abstract class BaseRepository
    {
        protected readonly TrainingContext _context;

        public BaseRepository(TrainingContext context)
        {
            _context = context;
        }
    }
}
