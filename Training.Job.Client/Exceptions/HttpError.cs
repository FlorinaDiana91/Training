using System.Runtime.Serialization;

namespace Training.Job.Client.Exceptions
{
    [DataContract]
    public class HttpError
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ExceptionMessage { get; set; }
        [DataMember]
        public string ExceptionType { get; set; }
        [DataMember]
        public string StackTrace { get; set; }
        [DataMember]
        public int HttpCode { get; set; }
        [DataMember]
        public HttpError InnerException { get; set; }
    }
}
