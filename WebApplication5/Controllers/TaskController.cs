using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Job.BusinessService;
using Training.Job.DataContracts;

namespace WebApplication5.Controllers
{
    public class TaskController : ApiController
    {
        private readonly JobService _jobService = new JobService();

        [Route("api/jobs/{id}")]
        [HttpGet]
        public async Task<TaskResource> GetJobAsync(int id)
        {
            var job = await _jobService.GetJobByID(id);

            if (job != null)
            {
                return job;
            }
            else
            {
                var resp = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("No job found with id {0}", id));
                throw new HttpResponseException(resp);
            }
        }
    }
}
