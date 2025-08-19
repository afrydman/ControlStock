using System;
using System.Configuration;

namespace Persistence.LogService
{
    public class Loggeable
    {
        protected static string _loggerId;

        public Loggeable(string loggerId)
        {

            LogService.Log.New(loggerId, String.Format("{0}\\{1}_{2}",ConfigurationManager.AppSettings["CarpetaLogs"], DateTime.Today.ToString("yyyy-MM-dd"), loggerId));
            _loggerId = loggerId;
        }
        public static void create(string id)
        {
            if (string.IsNullOrEmpty(_loggerId))
            {
                

            LogService.Log.New(id, String.Format("{0}\\{1}_{2}", ConfigurationManager.AppSettings["CarpetaLogs"], DateTime.Today.ToString("yyyy-MM-dd"), id));
            _loggerId = id;
            }
        }

        public static void Log(string message)
        {
            LogService.Log.Write(_loggerId, message);
        }

        public void Close()
        {
            LogService.Log.Close(_loggerId);
        }
    }
}
