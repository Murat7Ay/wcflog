using System.IO;

namespace LogWCF.LogWriter
{
    public static class WriteToTxt
    {
        public static void WriteToFile(string text)
        {
            using (var writetext = new StreamWriter(@"c:\apideneme\rrLogwcf.txt", true))
            {
                writetext.WriteLine(text);
            }
        }
    }
}