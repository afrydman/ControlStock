using System.Collections.Generic;

namespace Persistence.LogService
{
    public class NullLogger: ILogger
    {
        public void Close()
        {
            
        }

        public void Log(string message)
        {
            
        }

        public void LogStrings(string section, IList<string> strings)
        {
            
        }

        public string FilePath { get; private set; }
    }
}
