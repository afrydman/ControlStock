using System;
using System.Collections.Generic;
using System.IO;

namespace SharedForms.LogService
{
    public class Logger : ILogger
    {
        private readonly StreamWriter _log;

        public Logger(string file)
        {
            _log = new StreamWriter(file, true);
            FilePath = file;
        }

        public void Close()
        {
            _log.Close();
        }

        public virtual void Log(string message)
        {
            _log.WriteLine(String.Format("{0} >>> {1}", DateTime.Now, message));
        }

        public virtual void LogStrings(string section, IList<string> strings)
        {
            foreach (var str in strings)
            {
                Log(String.Format("{0} >>> {1}", section, str));
            }
        }

        public string FilePath { get; private set; }
    }
}
