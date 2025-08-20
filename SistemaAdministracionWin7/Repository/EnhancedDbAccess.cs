using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Persistence;
using Persistence.LogService;
using Repository.ConnectionFactoryStuff;

namespace Repository
{
    /// <summary>
    /// Enhanced database access layer with centralized try-catch logic and comprehensive logging
    /// </summary>
    public static class EnhancedDbAccess
    {
        private static readonly DatabaseLogger _logger = LoggerFactory.GetDatabaseLogger("EnhancedDbAccess");

        #region Dapper-based methods (recommended approach)

        /// <summary>
        /// Execute a query and return a single result with enhanced error handling and logging
        /// </summary>
        public static T QueryFirstOrDefault<T>(string connectionString, string sql, object param = null, string operationName = null)
        {
            return ExecuteWithLogging(() =>
            {
                using (var connection = ConnectionFactory.CreateConnection(connectionString))
                {
                    return connection.QueryFirstOrDefault<T>(sql, param);
                }
            }, operationName ?? "QueryFirstOrDefault", sql, param, default(T));
        }

        /// <summary>
        /// Execute a query and return multiple results with enhanced error handling and logging
        /// </summary>
        public static IEnumerable<T> Query<T>(string connectionString, string sql, object param = null, string operationName = null)
        {
            return ExecuteWithLogging(() =>
            {
                using (var connection = ConnectionFactory.CreateConnection(connectionString))
                {
                    return connection.Query<T>(sql, param);
                }
            }, operationName ?? "Query", sql, param, new List<T>());
        }

        /// <summary>
        /// Execute a non-query command (INSERT, UPDATE, DELETE) with enhanced error handling and logging
        /// </summary>
        public static int Execute(string connectionString, string sql, object param = null, string operationName = null)
        {
            return ExecuteWithLogging(() =>
            {
                using (var connection = ConnectionFactory.CreateConnection(connectionString))
                {
                    return connection.Execute(sql, param);
                }
            }, operationName ?? "Execute", sql, param, 0);
        }

        /// <summary>
        /// Execute a query and return the count of affected rows with enhanced error handling and logging
        /// </summary>
        public static bool ExecuteNonQuery(string connectionString, string sql, object param = null, string operationName = null)
        {
            var rowsAffected = Execute(connectionString, sql, param, operationName);
            return rowsAffected > 0;
        }

        #endregion

        #region Legacy Conexion support methods

        /// <summary>
        /// Enhanced wrapper for Conexion.ExcuteText with proper error handling and logging
        /// </summary>
        public static SqlDataReader ExecuteTextWithLogging(string sql, List<SqlParameter> parameters = null, bool useLocal = true, string operationName = null)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, operationName ?? "ExecuteText", new { SQL = sql });

            try
            {
                _logger.LogDebug(string.Format("Executing SQL query: {0}", sql), LogOperations.DB_SELECT);
                
                var result = Conexion.ExcuteText(sql, parameters, useLocal);
                
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, result != null, null);
                
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _logger.LogWarning(string.Format("Slow query detected: {0} took {1}ms", operationName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, null, ex.Message);
                _logger.LogError(string.Format("Database query failed: {0}", operationName), ex, LogOperations.DB_SELECT);
                throw new DatabaseOperationException(string.Format("Query operation '{0}' failed", operationName), ex, sql, parameters);
            }
        }

        /// <summary>
        /// Enhanced wrapper for Conexion.ExcuteReader with proper error handling and logging
        /// </summary>
        public static SqlDataReader ExecuteReaderWithLogging(string storedProcedure, List<SqlParameter> parameters = null, bool useLocal = true, string operationName = null)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, operationName ?? "ExecuteReader", new { StoredProcedure = storedProcedure });

            try
            {
                _logger.LogDebug(string.Format("Executing stored procedure: {0}", storedProcedure), LogOperations.DB_SELECT);
                
                var result = Conexion.ExcuteReader(storedProcedure, parameters, useLocal);
                
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, result != null, null);
                
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _logger.LogWarning(string.Format("Slow stored procedure: {0} took {1}ms", operationName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, null, ex.Message);
                _logger.LogError(string.Format("Stored procedure execution failed: {0}", operationName), ex, LogOperations.DB_SELECT);
                throw new DatabaseOperationException(string.Format("Stored procedure '{0}' failed", operationName), ex, storedProcedure, parameters);
            }
        }

        /// <summary>
        /// Enhanced wrapper for Conexion.ExecuteNonQuery with proper error handling and logging
        /// </summary>
        public static bool ExecuteNonQueryWithLogging(string command, List<SqlParameter> parameters = null, bool isStoredProcedure = true, bool useLocal = true, string operationName = null)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationType = isStoredProcedure ? LogOperations.DB_UPDATE : LogOperations.DB_EXECUTE;
            var operationId = _logger.StartOperation(operationType, operationName ?? "ExecuteNonQuery", new { Command = command, IsStoredProcedure = isStoredProcedure });

            try
            {
                _logger.LogDebug(string.Format("Executing {0}: {1}", (isStoredProcedure ? "stored procedure" : "SQL command"), command), operationType);
                
                var result = Conexion.ExecuteNonQuery(command, parameters, isStoredProcedure, useLocal);
                
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, operationType, result, result ? 1 : 0);
                
                if (stopwatch.ElapsedMilliseconds > 2000)
                {
                    _logger.LogWarning(string.Format("Slow {0}: {1} took {2}ms", (isStoredProcedure ? "stored procedure" : "command"), operationName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, operationType, false, 0, ex.Message);
                _logger.LogError(string.Format("{0} execution failed: {1}", (isStoredProcedure ? "Stored procedure" : "SQL command"), operationName), ex, operationType);
                throw new DatabaseOperationException(string.Format("{0} '{1}' failed", (isStoredProcedure ? "Stored procedure" : "Command"), operationName), ex, command, parameters);
            }
        }

        /// <summary>
        /// Enhanced wrapper for Conexion.GetDataTable with proper error handling and logging
        /// </summary>
        public static DataTable GetDataTableWithLogging(string storedProcedure, List<SqlParameter> parameters = null, string operationName = null)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, operationName ?? "GetDataTable", new { StoredProcedure = storedProcedure });

            try
            {
                _logger.LogDebug(string.Format("Executing stored procedure for DataTable: {0}", storedProcedure), LogOperations.DB_SELECT);
                
                var result = Conexion.GetDataTable(storedProcedure, parameters);
                
                stopwatch.Stop();
                var rowCount = result != null ? result.Rows.Count : 0;
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, true, rowCount);
                
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _logger.LogWarning(string.Format("Slow DataTable operation: {0} took {1}ms for {2} rows", operationName, stopwatch.ElapsedMilliseconds, rowCount), "PERFORMANCE");
                }

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, 0, ex.Message);
                _logger.LogError(string.Format("DataTable operation failed: {0}", operationName), ex, LogOperations.DB_SELECT);
                throw new DatabaseOperationException(string.Format("DataTable operation '{0}' failed", operationName), ex, storedProcedure, parameters);
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Generic method to execute database operations with comprehensive logging and error handling
        /// </summary>
        private static T ExecuteWithLogging<T>(Func<T> operation, string operationName, string sql = null, object parameters = null, T defaultValue = default(T))
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, operationName, new { SQL = sql });

            try
            {
                _logger.LogDebug(string.Format("Starting database operation: {0}", operationName), operationName);
                
                var result = operation();
                
                stopwatch.Stop();
                
                // Determine success and count based on result type
                bool success = true;
                int? count = null;
                
                if (result is IEnumerable<object>)
                {
                    var enumerable = result as IEnumerable<object>;
                    if (enumerable != null)
                    {
                        var list = enumerable.ToList();
                        count = list.Count;
                    }
                }
                else if (result is int)
                {
                    var intResult = (int)(object)result;
                    count = intResult;
                    success = intResult >= 0;
                }
                else if (result == null)
                {
                    success = false;
                }

                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, success, count);
                
                // Log performance warnings
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _logger.LogWarning(string.Format("Slow database operation: {0} took {1}ms", operationName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }
                else
                {
                    _logger.LogDebug(string.Format("Database operation completed: {0} in {1}ms", operationName, stopwatch.ElapsedMilliseconds), operationName);
                }

                return result;
            }
            catch (ConnectionException ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, null, ex.Message);
                _logger.LogError(string.Format("Database connection failed for operation: {0}", operationName), ex, "CONNECTION_ERROR");
                throw new DatabaseOperationException(string.Format("Connection failed for operation '{0}'", operationName), ex, sql, parameters);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, null, ex.Message);
                _logger.LogError(string.Format("Database operation failed: {0}", operationName), ex, operationName);
                throw new DatabaseOperationException(string.Format("Database operation '{0}' failed", operationName), ex, sql, parameters);
            }
        }

        /// <summary>
        /// Log connection information for debugging purposes
        /// </summary>
        public static void LogConnectionInfo(string connectionString, string context = null)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                var serverInfo = string.Format("Server: {0}, Database: {1}", builder.DataSource, builder.InitialCatalog);
                _logger.LogDebug(string.Format("Connection info{0}: {1}", (context != null ? string.Format(" for {0}", context) : ""), serverInfo), "CONNECTION_INFO");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(string.Format("Failed to log connection info: {0}", ex.Message), "CONNECTION_INFO");
            }
        }

        /// <summary>
        /// Execute a transaction with proper logging and rollback handling
        /// </summary>
        public static T ExecuteInTransaction<T>(string connectionString, Func<IDbConnection, IDbTransaction, T> operation, string operationName = null)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_TRANSACTION, operationName ?? "Transaction", null);

            IDbConnection connection = null;
            IDbTransaction transaction = null;

            try
            {
                _logger.LogDebug(string.Format("Starting transaction: {0}", operationName), LogOperations.DB_TRANSACTION);
                
                connection = ConnectionFactory.CreateConnection(connectionString);
                transaction = connection.BeginTransaction();
                
                var result = operation(connection, transaction);
                
                transaction.Commit();
                stopwatch.Stop();
                
                _logger.CompleteOperation(operationId, LogOperations.DB_TRANSACTION, true, null);
                _logger.LogDebug(string.Format("Transaction completed successfully: {0} in {1}ms", operationName, stopwatch.ElapsedMilliseconds), LogOperations.DB_TRANSACTION);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                
                try
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        _logger.LogWarning(string.Format("Transaction rolled back due to error: {0}", operationName), LogOperations.DB_TRANSACTION);
                    }
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(string.Format("Failed to rollback transaction: {0}", operationName), rollbackEx, LogOperations.DB_TRANSACTION);
                }
                
                _logger.CompleteOperation(operationId, LogOperations.DB_TRANSACTION, false, null, ex.Message);
                _logger.LogError(string.Format("Transaction failed: {0}", operationName), ex, LogOperations.DB_TRANSACTION);
                
                throw new DatabaseOperationException(string.Format("Transaction '{0}' failed", operationName), ex);
            }
            finally
            {
                try
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                    }
                }
                catch (Exception disposeEx)
                {
                    _logger.LogWarning(string.Format("Error disposing transaction resources: {0}", disposeEx.Message), LogOperations.DB_TRANSACTION);
                }
            }
        }

        #endregion
    }
}