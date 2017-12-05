using System.Runtime.Serialization;

namespace VRS.WebApi
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Successful { get; set; }
        [DataMember]
        public string Message { get; set; }

        public Result(bool successful, string message)
        {
            Successful = successful;
            Message = message;
        }
    }
}