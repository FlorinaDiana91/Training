using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace Training.Job.Client
{
    public class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Deserialize<T>(json);
        }

        public static string Serialize<T>(T data)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(data);
        }

        public static object Deserialize(string json, Type type)
        {

            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(type);
                return serializer.ReadObject(stream);
            }
        }
    }
}
