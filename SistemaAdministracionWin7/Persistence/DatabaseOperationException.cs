using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistence
{
    /// <summary>
    /// Custom exception for database operations with enhanced context information
    /// </summary>
    public class DatabaseOperationException : Exception
    {
        public string SqlCommand { get; }
        public List<SqlParameter> Parameters { get; }
        public string OperationName { get; }
        public DateTime Timestamp { get; }

        public DatabaseOperationException(string message) : base(message)
        {
            Timestamp = DateTime.Now;
        }

        public DatabaseOperationException(string message, Exception innerException) : base(message, innerException)
        {
            Timestamp = DateTime.Now;
        }

        public DatabaseOperationException(string message, Exception innerException, string sqlCommand, List<SqlParameter> parameters = null) 
            : base(message, innerException)
        {
            SqlCommand = sqlCommand;
            Parameters = parameters;
            Timestamp = DateTime.Now;
        }

        public DatabaseOperationException(string message, Exception innerException, string sqlCommand, object parameters) 
            : base(message, innerException)
        {
            SqlCommand = sqlCommand;
            Timestamp = DateTime.Now;
            
            // Convert anonymous object parameters to string for logging
            if (parameters != null)
            {
                ParametersInfo = parameters.ToString();
            }
        }

        public string ParametersInfo { get; private set; }

        public override string ToString()
        {
            var details = base.ToString();
            
            if (!string.IsNullOrEmpty(SqlCommand))
            {
                details += $"\nSQL Command: {SqlCommand}";
            }
            
            if (Parameters != null && Parameters.Count > 0)
            {
                details += "\nParameters:";
                foreach (var param in Parameters)
                {
                    details += $"\n  {param.ParameterName}: {param.Value}";
                }
            }
            
            if (!string.IsNullOrEmpty(ParametersInfo))
            {
                details += $"\nParameters: {ParametersInfo}";
            }
            
            details += $"\nTimestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}";
            
            return details;
        }
    }
}