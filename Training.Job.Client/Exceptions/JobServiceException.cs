using System;
using System.Text;
using Training.Job.Client.Interfaces;

namespace Training.Job.Client.Exceptions
{
    public class JobServiceException : Exception, IExceptionDetail
    {
        public HttpError HttpError { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public override string Message
        {
            get
            {
                if (HttpError == null)
                {
                    return null;
                }
                else
                {
                    return HttpError.Message;
                }
            }
        }

        public JobServiceException(System.Net.HttpStatusCode status, HttpError error)
        {
            this.StatusCode = status;
            this.HttpError = error;
        }

        public JobServiceException(System.Net.HttpStatusCode status, string errorJson)
        {
            this.StatusCode = status;
            this.HttpError = JsonHelper.Deserialize<HttpError>(errorJson);
        }

        public string GetDetail()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Http Status Code: " + StatusCode.ToString());
            sb.AppendLine("Service Exception Type: " + (HttpError != null ? HttpError.ExceptionType : "none"));
            sb.AppendLine("Http Error Message: " + (HttpError != null ? HttpError.Message : " empty"));
            sb.AppendLine("Wheelhouse Service Exception Message: " + (HttpError != null ? HttpError.ExceptionMessage : " empty"));
            sb.AppendLine("Service Stack Trace: " + (HttpError != null ? HttpError.StackTrace : " empty"));
            return sb.ToString();
        }
    }
}
