using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dapper;
using Persistence;
using Persistence.LogService;

namespace Repository
{
    /// <summary>
    /// Enhanced base repository class with centralized database access, error handling, and logging
    /// </summary>
    public abstract class EnhancedDbRepository
    {
        protected readonly string _connectionString;
        protected readonly DatabaseLogger _logger;
        protected readonly string _repositoryName;

        public abstract string DEFAULT_SELECT { get; }
        public virtual string tipoRepository { get { return this.ToString(); } }

        protected EnhancedDbRepository(bool useLocal = true, string repositoryName = null)
        {
            _repositoryName = repositoryName ?? this.GetType().Name;
            _logger = LoggerFactory.GetDatabaseLogger(_repositoryName);

            string connString = useLocal
                ? ConfigurationManager.ConnectionStrings["Local"].ConnectionString
                : ConfigurationManager.ConnectionStrings["remote"].ConnectionString;

            if (string.IsNullOrEmpty(connString))
                throw new ArgumentNullException("connectionString", "No puede usar un DbRepository sin indicar un connectionString.");

            _connectionString = connString;
            
            // Log connection info on initialization
            EnhancedDbAccess.LogConnectionInfo(_connectionString, _repositoryName);
        }

        #region Enhanced Dapper Methods

        /// <summary>
        /// Execute a query and return a single result with automatic error handling and logging
        /// </summary>
        protected T QueryFirstOrDefault<T>(string sql, object param = null, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.QueryFirstOrDefault", _repositoryName);
            return EnhancedDbAccess.QueryFirstOrDefault<T>(_connectionString, sql, param, operation);
        }

        /// <summary>
        /// Execute a query and return multiple results with automatic error handling and logging
        /// </summary>
        protected IEnumerable<T> Query<T>(string sql, object param = null, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.Query", _repositoryName);
            return EnhancedDbAccess.Query<T>(_connectionString, sql, param, operation);
        }

        /// <summary>
        /// Execute a non-query command with automatic error handling and logging
        /// </summary>
        protected int Execute(string sql, object param = null, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.Execute", _repositoryName);
            return EnhancedDbAccess.Execute(_connectionString, sql, param, operation);
        }

        /// <summary>
        /// Execute a command and return true if rows were affected
        /// </summary>
        protected bool ExecuteNonQuery(string sql, object param = null, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.ExecuteNonQuery", _repositoryName);
            return EnhancedDbAccess.ExecuteNonQuery(_connectionString, sql, param, operation);
        }

        #endregion

        #region Legacy Conexion Support Methods

        /// <summary>
        /// Execute SQL text with enhanced error handling and logging (legacy support)
        /// </summary>
        protected SqlDataReader ExecuteText(string sql, List<SqlParameter> parameters = null, bool useLocal = true, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.ExecuteText", _repositoryName);
            return EnhancedDbAccess.ExecuteTextWithLogging(sql, parameters, useLocal, operation);
        }

        /// <summary>
        /// Execute stored procedure with enhanced error handling and logging (legacy support)
        /// </summary>
        protected SqlDataReader ExecuteReader(string storedProcedure, List<SqlParameter> parameters = null, bool useLocal = true, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.ExecuteReader", _repositoryName);
            return EnhancedDbAccess.ExecuteReaderWithLogging(storedProcedure, parameters, useLocal, operation);
        }

        /// <summary>
        /// Execute non-query with enhanced error handling and logging (legacy support)
        /// </summary>
        protected bool ExecuteNonQueryLegacy(string command, List<SqlParameter> parameters = null, bool isStoredProcedure = true, bool useLocal = true, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.ExecuteNonQueryLegacy", _repositoryName);
            return EnhancedDbAccess.ExecuteNonQueryWithLogging(command, parameters, isStoredProcedure, useLocal, operation);
        }

        /// <summary>
        /// Get DataTable with enhanced error handling and logging (legacy support)
        /// </summary>
        protected DataTable GetDataTable(string storedProcedure, List<SqlParameter> parameters = null, string operationName = null)
        {
            var operation = operationName ?? string.Format("{0}.GetDataTable", _repositoryName);
            return EnhancedDbAccess.GetDataTableWithLogging(storedProcedure, parameters, operation);
        }

        #endregion

        #region Transaction Support

        /// <summary>
        /// Execute multiple operations within a transaction with automatic rollback on error
        /// </summary>
        protected T ExecuteInTransaction<T>(Func<IDbConnection, IDbTransaction, T> operation, string operationName = null)
        {
            var name = operationName ?? string.Format("{0}.Transaction", _repositoryName);
            return EnhancedDbAccess.ExecuteInTransaction(_connectionString, operation, name);
        }

        /// <summary>
        /// Execute multiple operations within a transaction (void return)
        /// </summary>
        protected void ExecuteInTransaction(Action<IDbConnection, IDbTransaction> operation, string operationName = null)
        {
            ExecuteInTransaction<bool>((conn, trans) =>
            {
                operation(conn, trans);
                return true;
            }, operationName);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Log a custom database operation for monitoring purposes
        /// </summary>
        protected void LogDatabaseOperation(string operationType, string details, bool success = true, int? recordsAffected = null)
        {
            if (success)
            {
                _logger.LogInfo(string.Format("{0}: {1}", operationType, details) + (recordsAffected.HasValue ? string.Format(" | {0} records", recordsAffected) : ""), operationType);
            }
            else
            {
                _logger.LogWarning(string.Format("{0} failed: {1}", operationType, details), operationType);
            }
        }

        /// <summary>
        /// Create a SqlParameter with automatic null value handling
        /// </summary>
        protected SqlParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// Create a list of SqlParameters from an anonymous object
        /// </summary>
        protected List<SqlParameter> CreateParameters(object parameters)
        {
            var paramList = new List<SqlParameter>();
            
            if (parameters != null)
            {
                var properties = parameters.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(parameters, null);
                    paramList.Add(CreateParameter(string.Format("@{0}", prop.Name), value));
                }
            }
            
            return paramList;
        }

        /// <summary>
        /// Validate that required parameters are not null or empty
        /// </summary>
        protected void ValidateParameters(params object[] nameValuePairs)
        {
            if (nameValuePairs.Length % 2 != 0)
                throw new ArgumentException("Parameters must be provided in name-value pairs");

            for (int i = 0; i < nameValuePairs.Length; i += 2)
            {
                var name = nameValuePairs[i] as string;
                var value = nameValuePairs[i + 1];

                if (value == null || (value is string && string.IsNullOrWhiteSpace(value.ToString())))
                {
                    var exception = new ArgumentNullException(name, string.Format("Parameter '{0}' cannot be null or empty", name));
                    _logger.LogError(string.Format("Parameter validation failed in {0}", _repositoryName), exception, "VALIDATION");
                    throw exception;
                }
            }
        }

        /// <summary>
        /// Log slow query performance for monitoring
        /// </summary>
        protected void LogSlowQuery(string queryName, long durationMs, int? recordCount = null)
        {
            if (durationMs > 1000)
            {
                var message = string.Format("Slow query detected: {0} took {1}ms", queryName, durationMs);
                if (recordCount.HasValue)
                {
                    message += string.Format(" for {0} records", recordCount);
                }
                _logger.LogWarning(message, "PERFORMANCE");
            }
        }

        #endregion

        #region Error Handling Helpers

        /// <summary>
        /// Handle and log common database exceptions with user-friendly messages
        /// </summary>
        protected Exception HandleDatabaseException(Exception ex, string operationName, string sql = null)
        {
            string userMessage;
            
            if (ex is SqlException)
            {
                userMessage = GetSqlExceptionMessage((SqlException)ex);
            }
            else if (ex is DatabaseOperationException)
            {
                // Already handled, just re-throw
                throw ex;
            }
            else if (ex is TimeoutException)
            {
                userMessage = "La operación tardó demasiado tiempo en completarse. Intente nuevamente.";
            }
            else
            {
                userMessage = "Ocurrió un error inesperado en la base de datos.";
            }

            _logger.LogError(string.Format("Database error in {0}: {1}", operationName, userMessage), ex, "DB_ERROR");
            return new DatabaseOperationException(string.Format("{0}: {1}", operationName, userMessage), ex, sql);
        }

        private string GetSqlExceptionMessage(SqlException sqlEx)
        {
            switch (sqlEx.Number)
            {
                case 2: // Connection timeout
                case -2:
                    return "No se pudo conectar a la base de datos. Verifique la conexión.";
                case 18456: // Login failed
                    return "Error de autenticación en la base de datos.";
                case 2627: // Primary key violation
                case 2601: // Unique constraint violation
                    return "Ya existe un registro con los mismos datos.";
                case 547: // Foreign key violation
                    return "No se puede completar la operación debido a referencias relacionadas.";
                case 515: // NULL value violation
                    return "Faltan datos requeridos para completar la operación.";
                default:
                    return string.Format("Error de base de datos: {0}", sqlEx.Message);
            }
        }

        #endregion
    }
}