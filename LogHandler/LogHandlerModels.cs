using System;

namespace LogWCF.LogHandler
{
    public class CorrelationModel
    {
        public Guid CorrelationGuid { get; set; }
        public long ResponseTicks { get; set; }
    }
}