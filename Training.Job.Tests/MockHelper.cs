using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Job.DAL.Interfaces;
using Training.Job.DataContracts;

namespace Training.Job.Tests
{
    public class MockHelper
    {
        public static Mock<IJobRepository> GetMockRepoForSuccess()
        {
            Mock<IJobRepository> repo = new Mock<IJobRepository>();

            repo.Setup(r => r.UpdateTaskResource(It.IsAny<TaskResource>()));
            repo.Setup(r => r.DeleteTaskResource(It.IsAny<int>()));
            repo.Setup(r => r.AddTaskResource(It.IsAny<DAL.DataModel.Task>()));
            repo.Setup(r => r.GetJobByID(It.IsAny<int>())).Returns(System.Threading.Tasks.Task.Run(() => SampleData.TaskQueueItems[0])); 
            repo.Setup(r => r.GetJobs()).Returns(GetTestQueue());
            return repo;
        }


        static Task<List<DAL.DataModel.Task>> GetTestQueue()
        {
            var queueItems =  new List<DAL.DataModel.Task>(SampleData.TaskQueueItems);
            return System.Threading.Tasks.Task.Run(() => queueItems);
        }
    }
}
