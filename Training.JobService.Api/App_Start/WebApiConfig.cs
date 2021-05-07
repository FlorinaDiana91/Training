using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Training.Job.BusinessService;
using Training.Job.DAL;
using Training.Job.DAL.Interfaces;
using Unity;
using Unity.AspNet.WebApi;
using Microsoft.Extensions.Logging;
using Training.JobService.Api.Controllers;

namespace Training.JobService.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container
                .RegisterType<IJobRepository, JobRepository>();
            container
                .RegisterType<IJobService, Training.Job.BusinessService.JobService>();
            container.RegisterType<NLog.ILogger, NLog.Logger>();

            //config.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(container);

            //this
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
