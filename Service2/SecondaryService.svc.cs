using System;
using System.Threading;

namespace LogWCF.Service2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SecondaryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SecondaryService.svc or SecondaryService.svc.cs at the Solution Explorer and start debugging.
    public class SecondaryService : ISecondaryService
    {
        public bool OfAge(int bornYear)
        {
            return DateTime.Now.Year - bornYear > 18;
        }



        public void Wait(int waitTime)
        {
            Thread.Sleep(waitTime*1000);
        }
    }
}
