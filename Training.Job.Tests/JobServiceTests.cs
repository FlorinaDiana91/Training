using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Job.DAL.Interfaces;
using Training.Job.DataContracts;

namespace Training.Job.Tests
{
    [TestClass]
    public class JobServiceTests
    {
        readonly BusinessService.JobService _service;
        public JobServiceTests()
        {
            _service = GetService(true);
        }

        BusinessService.JobService GetService(bool forSuccess = true)
        {
            Mock<IJobRepository> repo = MockHelper.GetMockRepoForSuccess();
            return new BusinessService.JobService(repo.Object);
        }

        [TestMethod]
        public void GetJobs_Success()
        {
            var result = _service.GetJobs();
            var viewResult = Xunit.Assert.IsType<Task<List<TaskResource>>>(result);

            Xunit.Assert.True(viewResult.Result != null);
            Xunit.Assert.True(SampleData.TaskQueueItems.Length == viewResult.Result.Count);
            Xunit.Assert.True(viewResult.Result[0].TaskID == 100);
        }


        [TestMethod]
        public void GetJobById_Success()
        {
            var result = _service.GetJobByID(100);
            var viewResult = Xunit.Assert.IsType<Task<TaskResource>>(result);

            Xunit.Assert.True(viewResult.Result != null);
            Xunit.Assert.True(viewResult.Result.TaskID == SampleData.TaskQueueItems[0].TaskID);
        }
    }
}
