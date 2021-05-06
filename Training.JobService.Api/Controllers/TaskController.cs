using System.Web.Http;
using System.Threading.Tasks;
using Training.Job.DataContracts;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Training.JobService.Api.Controllers
{
    public class TaskController : ApiController
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Job.BusinessService.JobService _jobService = new Training.Job.BusinessService.JobService();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getJob/{id}")]
        [Produces("application/json")]

        public async Task<IHttpActionResult> GetJobAsync(int id)
        {
            var job = await _jobService.GetJobByID(id);
            ApiResult<TaskResource> result = new ApiResult<TaskResource>();

            try
            {
                result.Success = true;
                result.Data = job;
            }
            catch (Exception ex)
            {
                result.Error = "Error during retrieval of task resources";
               _logger.Error(ex, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(job);
        }

       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getJobs")]
        [Produces("application/json")]
        public async Task<IHttpActionResult> GetJobsAsync()
        {
            var job = await _jobService.GetJobs();
            ApiResult<List<TaskResource>> result = new ApiResult<List<TaskResource>>();

            try
            {
                result.Success = true;
                result.Data = job;
            }
            catch (Exception ex)
            {
                result.Error = "Error during retrieval of task resources";
                //_logger.LogError(ex, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(job);
        }
    }
}
