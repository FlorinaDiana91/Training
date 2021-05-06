using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Training.Job.DataContracts;

namespace ConsoleApp1
{
    class Program
    {
        private static async Task ReadFromWebApi()
        {
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var resp2 = await client.GetAsync("api/getJobs/");
                resp2.EnsureSuccessStatusCode();
                var aaa = resp2.Content;
                string result = await aaa.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        static async Task<List<TaskResource>> RunAsync()
        {
            List<TaskResource> reservationList = new List<TaskResource>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44384/api/getJobs"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<TaskResource>>(apiResponse);
                }
            }

            return reservationList;

            //// Update port # in the following line.
            //client.BaseAddress = new Uri("http://localhost:64195/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
        }


        //    static TaskResource GetJob(int id)
        //{
        //    JobClient client;
        //    var f = System.Configuration.ConfigurationManager.AppSettings["JobServiceURL"].ToString();
        //    var g = new JobClient(f);
        //    var result = g.GetJob(1);
        //    return result;
        //}

        static void Main(string[] args)
        { 


            try
            {
                  ReadFromWebApi().GetAwaiter().GetResult();

            //    var f = RunAsync().GetAwaiter().GetResult();
                //TaskResource resource = null;
                //client.GetJob(1);

                //   GetJob(1);
            }
            catch (Exception ex)
            {
                // TODO need to handle the exception here
                throw;
            }

        }

    }
}
