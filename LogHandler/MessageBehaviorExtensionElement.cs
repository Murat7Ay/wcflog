using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace LogWCF.LogHandler
{
    public class MessageBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(MessageBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new MessageBehavior();
        }
    }
}