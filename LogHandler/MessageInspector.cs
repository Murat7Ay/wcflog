using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;
using System.Xml;
using LogWCF.LogWriter;
using Newtonsoft.Json;


namespace LogWCF.LogHandler
{
    public class MessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var webContext = WebOperationContext.Current;
            var context = OperationContext.Current;
            var properties = context.IncomingMessageProperties;
            var httpRequest = properties["httpRequest"];
            var correlationGuid = Guid.NewGuid();
            request.Headers.Add(MessageHeader.CreateHeader("Correlation-GUID", "http://tempuri.org", correlationGuid)); 
            XmlWriterSettings settings = new XmlWriterSettings { Encoding = System.Text.Encoding.UTF8 };
            StringWriter sw = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sw, settings);
            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
            //Create a copy of the message in order to continue the handling of te SOAP           
            request = buffer.CreateMessage();
            request.WriteMessage(writer);
            //Recreate the message   
            writer.Flush();
            //Flush the contents of the writer so that the stream gets updated  
            //you can log the str to the database   
            var str = sw.ToString();

            //Xml to json
            var xml = new XmlDocument();
            xml.LoadXml(str);
            var jsonXmlRequest = JsonConvert.SerializeObject(xml, Newtonsoft.Json.Formatting.Indented);
            WriteToTxt.WriteToFile(jsonXmlRequest);



            request = buffer.CreateMessage();

            XmlDictionaryReader xmlDict = request.GetReaderAtBodyContents();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlDict.ReadOuterXml());
            request = buffer.CreateMessage();
            WriteToTxt.WriteToFile(str);

            return new CorrelationModel { CorrelationGuid = correlationGuid, ResponseTicks = DateTime.Now.Ticks };
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var correlationModel = (CorrelationModel)correlationState;
            if (reply != null)
            {
                //Correlation Guid for track
                var correlationGuid = MessageHeader.CreateHeader("Correlation-GUID", "http://tempuri.org", correlationModel.CorrelationGuid.ToString());
                reply.Headers.Add(correlationGuid);
                XmlWriterSettings settings = new XmlWriterSettings { Encoding = System.Text.Encoding.UTF8 };
                StringWriter sw = new StringWriter();
                XmlWriter writer = XmlWriter.Create(sw, settings);
                MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
                //Create a copy of the message in order to continue the handling of te SOAP           
                reply = buffer.CreateMessage();
                reply.WriteMessage(writer);
                //Recreate the message   
                writer.Flush();
                //Flush the contents of the writer so that the stream gets updated  
                //you can log the str to the database   
                var str = sw.ToString();
                WriteToTxt.WriteToFile(str);
                reply = buffer.CreateMessage();
            }
            else
            {
                WriteToTxt.WriteToFile("No response xml for this request");
            }

            WriteToTxt.WriteToFile("Response time:" + new TimeSpan(DateTime.Now.Ticks - correlationModel.ResponseTicks));
        }
    }
}