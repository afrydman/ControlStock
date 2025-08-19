using System;
using System.Configuration;

namespace Sync
{
    public class LoggeableJob
    {
        protected string _loggerId;

        public LoggeableJob(string loggerId)
        {

            SharedForms.LogService.Log.New(loggerId, String.Format("{0}\\{1}_{2}",ConfigurationManager.AppSettings["CarpetaLogs"], DateTime.Today.ToString("yyyy-MM-dd"), loggerId));
            _loggerId = loggerId;
        }

        public void Log(string message)
        {
            SharedForms.LogService.Log.Write(_loggerId, message);
        }

        public void Close()
        {
            SharedForms.LogService.Log.Close(_loggerId);
        }
    }
}
