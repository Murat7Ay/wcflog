using System;
using LogWCF.LogHandler;

namespace LogWCF.Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LogService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LogService.svc or LogService.svc.cs at the Solution Explorer and start debugging.
    [OutputBehavior]
    public class LogService : ILogService
    {
        public ResponseModel Info(RequestModel request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.SurName)||request.Age<=0)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorText = "Error on validating request"
                };
            }
            return new ResponseModel
            {
                IsSuccess = true,
                BornYear = DateTime.Now.Year-request.Age,
                FullName = request.Name+" "+request.SurName
            };
        }
    }
}
