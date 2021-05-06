using System;
using System.Net;
using System.Net.Http;
using Training.Job.Client;
using Training.Job.DataContracts;

namespace ConsoleApp1
{
    class ConsumeTaskAsync
    {
        string _scriptServiceUrl = string.Empty;

        public ConsumeTaskAsync()
        {
            InitializeServiceReferences();
        }
         
        private void InitializeServiceReferences()
        {
            _scriptServiceUrl = System.Configuration.ConfigurationManager.AppSettings["JobServiceUri"].ToString();
        }

        public TaskResource GetJob(int id)
        {
            return new JobClient(_scriptServiceUrl).GetJob(id);
        }
    }

}

