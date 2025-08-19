using System.Collections.Generic;

namespace Persistence.LogService
{
    public interface ILogger
    {
        void Close();
        void Log(string message);
        void LogStrings(string section, IList<string> strings);
        string FilePath { get; }
    }
}
