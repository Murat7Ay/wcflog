using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LogWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILogService" in both code and config file together.
    [ServiceContract]
    public interface ILogService
    {
        [OperationContract]
        ResponseModel Info(RequestModel request);

    }

    public class RequestModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
    }

    public class ResponseModel : Status
    {
        public string FullName { get; set; }
        public int BornYear { get; set; }
    }

    public class Status
    {
        public bool IsSuccess  { get; set; }
        public string ErrorText { get; set; }

    }
}
