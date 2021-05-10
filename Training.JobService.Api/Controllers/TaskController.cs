using System.Web.Http;
using System.Threading.Tasks;
using Training.Job.DataContracts;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Training.Job.BusinessService;

namespace Training.JobService.Api.Controllers
{
    public class TaskController : ApiController
    {
        readonly NLog.ILogger _logger;
        private readonly Job.BusinessService.IJobService _jobService;

        public TaskController(IJobService jobService, NLog.ILogger log)
        {
            this._jobService = jobService;
            this._logger = log;
        }

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
                _logger.Info("Success");
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
                _logger.Error(ex, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(job);
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/updateTaskResource")]
        [Produces("application/json")]
        public async Task<IHttpActionResult> UpdateTaskResource([System.Web.Http.FromBody] TaskResource task)
        {
            ApiResult<List<TaskResource>> result = new ApiResult<List<TaskResource>>();
            try
            {
                result.Success = true;
                await _jobService.UpdateTaskResource(task);
                return Ok();
            }
            catch (Exception ex)
            {

                result.Error = "Error during updating of task resource";
                _logger.Error(ex, result.Error);
                return BadRequest(result.Error);
            }
        }


        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/deleteTaskResource/{taskID}")]
        [Produces("application/json")]
        public async Task<IHttpActionResult> DeleteTaskResource(int taskID)
        {
            ApiResult<List<TaskResource>> result = new ApiResult<List<TaskResource>>();
            try
            {
                result.Success = true;
                await _jobService.DeleteTaskResource(taskID);
                return Ok();
            }
            catch (Exception ex)
            {

                result.Error = "Error during removing of task resource";
                _logger.Error(ex, result.Error);
                return BadRequest(result.Error);
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/addTaskResource")]
        [Produces("application/json")]
        public async Task<IHttpActionResult> AddTaskResource([System.Web.Http.FromBody] Training.Job.DAL.DataModel.Task task)
        {
            ApiResult<List<TaskResource>> result = new ApiResult<List<TaskResource>>();
            try
            {
                result.Success = true;
                await _jobService.AddTaskResource(task);
                return Ok();
            }
            catch (Exception ex)
            {

                result.Error = "Error during creation of task resources";
                _logger.Error(ex, result.Error);
                return BadRequest(result.Error);
            }
        }
    }
}
