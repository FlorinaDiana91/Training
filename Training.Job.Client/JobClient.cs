using System;
using System.Collections.Generic;

namespace Training.Job.Client
{
    public class JobClient
    {
        internal string _serviceUrl = string.Empty;
        public JobClient(string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                throw new NullReferenceException("No service URL was provided");
            }

            _serviceUrl = serviceUrl;
        }

        public JobClient()
        {
            LoadServiceURLFromConfigFile();
            if (string.IsNullOrWhiteSpace(_serviceUrl))
            {
                throw new NullReferenceException("Could not find service url in config file");
            }
        }

        void LoadServiceURLFromConfigFile()
        {
            _serviceUrl = System.Configuration.ConfigurationManager.AppSettings["JobServiceURL"];
        }

        public DataContracts.TaskResource GetJob(int id)
        {
            var urlSegments = new Dictionary<string, string>()
                {
                    {
                        "id",
                        id.ToString()
                    }
                };

            return ServiceClient.MakeServiceCall<DataContracts.TaskResource>(_serviceUrl, "api/getJob/{id}/", RestSharp.Method.GET, urlSegments);
        }

        public List<DataContracts.TaskResource> GetJobs()
        {
            return ServiceClient.MakeServiceCall<List<DataContracts.TaskResource>>(_serviceUrl, "api/getJobs/", RestSharp.Method.GET);
        }
    }
}
