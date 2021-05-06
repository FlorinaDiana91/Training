using Newtonsoft.Json;

namespace Training.JobService.Api
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public bool Success { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
}