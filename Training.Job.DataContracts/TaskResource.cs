using System;
using System.Runtime.Serialization;

namespace Training.Job.DataContracts
{
    [DataContract]
    public class TaskResource
    {
        [DataMember]
        public int TaskID { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public bool TaskProcessed { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public DateTime? TaskDate { get; set; }
    }
}
