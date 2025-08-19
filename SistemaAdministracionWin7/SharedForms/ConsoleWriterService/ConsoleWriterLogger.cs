using System;

namespace SharedForms.ConsoleWriterService
{
    public class ConsoleWriterLogger : ConsoleWriter
    {
        public override void printWithDateTime(string messageToPrint)
        {
            Console.WriteLine(DateTime.Now.ToShortDateString() + ": " + messageToPrint);
        }
    }
}
