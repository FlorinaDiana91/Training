using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Training.Job.DataContracts;

namespace Training.Job.WeeklyScheduler
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string WebApiUrl { get { return System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]; } }

        static async Task<List<TaskResource>> GetJobsAsync()
        {
            List<TaskResource> reservationList = new List<TaskResource>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{WebApiUrl}/api/getJobs"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<TaskResource>>(apiResponse);
                }
            }

            return reservationList;
        }

        static async Task<bool> UpdateAsync(TaskResource task)
        {
            task.TaskProcessed = true;
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebApiUrl);
            var result = await httpClient.PutAsync("api/updateTaskResource", task, new JsonMediaTypeFormatter());

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            var createDate = DateTime.Now.AddDays(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["EndOfWeek"]));

            logger.Debug("Started weekly job service");
            Console.WriteLine("Started...");

            var jobs = GetJobsAsync().GetAwaiter().GetResult().Where(f => !f.TaskProcessed && f.TaskDate.HasValue && f.TaskDate.Value > createDate).ToList();

            if (jobs != null)
            {
                foreach (var job in jobs)
                {
                    switch (job.TypeName)
                    {
                        case "Notification":
                            UpdateAsync(job).GetAwaiter().GetResult();
                            break;
                        case "Job":
                            UpdateAsync(job).GetAwaiter().GetResult();
                            break;

                        default:
                            break;
                    }
                }

            }

            logger.Debug("No of jobs to run: " + jobs.Count);
            MailHandler.SendWeeklyMail(jobs);
            Console.WriteLine("Finished");
            logger.Debug("Finished");
        }
    }
}
