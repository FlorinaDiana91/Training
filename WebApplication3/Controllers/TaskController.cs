using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Job.BusinessService;
using Training.Job.DataContracts;

namespace Training.Job.Service.Controllers
{
    public class TaskController : ApiController
    {
        private readonly JobService _jobService= new JobService();

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
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
