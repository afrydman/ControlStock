using System;
using System.Collections.Generic;
using System.Diagnostics;
using DTO.BusinessEntities;
using Persistence.LogService;

namespace Repository
{
    /// <summary>
    /// Base repository class with enhanced logging capabilities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class LoggedRepository<T> : IGenericRepository<T> where T : GenericObject
    {
        protected readonly DatabaseLogger _logger;
        protected readonly string _entityName;

        protected LoggedRepository(string entityName)
        {
            _entityName = entityName;
            _logger = LoggerFactory.GetDatabaseLogger($"{entityName}Repository");
        }

        protected void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        public virtual bool Insert(T entity)
        {
            var operationId = _logger.StartOperation(LogOperations.DB_INSERT, _entityName, new { ID = entity.ID });
            
            try
            {
                var success = ExecuteInsert(entity);
                _logger.CompleteOperation(operationId, LogOperations.DB_INSERT, success, success ? 1 : 0);
                
                if (success)
                {
                    LogEntityOperation("INSERT", entity.ID, "Entity created successfully");
                }
                
                return success;
            }
            catch (Exception ex)
            {
                _logger.CompleteOperation(operationId, LogOperations.DB_INSERT, false, 0, ex.Message);
                throw;
            }
        }

        public virtual bool Update(T entity)
        {
            var operationId = _logger.StartOperation(LogOperations.DB_UPDATE, _entityName, new { ID = entity.ID });
            
            try
            {
                var success = ExecuteUpdate(entity);
                _logger.CompleteOperation(operationId, LogOperations.DB_UPDATE, success, success ? 1 : 0);
                
                if (success)
                {
                    LogEntityOperation("UPDATE", entity.ID, "Entity updated successfully");
                }
                
                return success;
            }
            catch (Exception ex)
            {
                _logger.CompleteOperation(operationId, LogOperations.DB_UPDATE, false, 0, ex.Message);
                throw;
            }
        }

        public virtual bool Delete(Guid id)
        {
            var operationId = _logger.StartOperation(LogOperations.DB_DELETE, _entityName, new { ID = id });
            
            try
            {
                var success = ExecuteDelete(id);
                _logger.CompleteOperation(operationId, LogOperations.DB_DELETE, success, success ? 1 : 0);
                
                if (success)
                {
                    LogEntityOperation("DELETE", id, "Entity deleted successfully");
                }
                
                return success;
            }
            catch (Exception ex)
            {
                _logger.CompleteOperation(operationId, LogOperations.DB_DELETE, false, 0, ex.Message);
                throw;
            }
        }

        public virtual T GetByID(Guid id)
        {
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, _entityName, new { ID = id });
            
            try
            {
                var entity = ExecuteGetByID(id);
                var found = entity != null;
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, true, found ? 1 : 0);
                
                if (!found)
                {
                    LogEntityOperation("SELECT", id, "Entity not found");
                }
                
                return entity;
            }
            catch (Exception ex)
            {
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, 0, ex.Message);
                throw;
            }
        }

        public virtual List<T> GetAll()
        {
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, _entityName, "GetAll");
            
            try
            {
                var entities = ExecuteGetAll();
                var count = entities?.Count ?? 0;
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, true, count);
                
                LogEntityOperation("SELECT_ALL", Guid.Empty, $"Retrieved {count} entities");
                
                return entities ?? new List<T>();
            }
            catch (Exception ex)
            {
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, 0, ex.Message);
                throw;
            }
        }

        // Abstract methods to be implemented by derived classes
        protected abstract bool ExecuteInsert(T entity);
        protected abstract bool ExecuteUpdate(T entity);
        protected abstract bool ExecuteDelete(Guid id);
        protected abstract T ExecuteGetByID(Guid id);
        protected abstract List<T> ExecuteGetAll();

        // Helper methods for logging
        protected void LogEntityOperation(string operation, Guid entityId, string details)
        {
            var businessLogger = LoggerFactory.GetBusinessLogger($"{_entityName}Business");
            businessLogger.LogProductOperation(operation, entityId.ToString(), _entityName, true, details);
        }

        protected void LogSlowQuery(string query, long durationMs)
        {
            if (durationMs > 1000)
            {
                _logger.CompleteOperation("", "SLOW_QUERY", true, null, $"Query: {query} took {durationMs}ms");
            }
        }

        protected TResult LoggedQuery<TResult>(Func<TResult> queryFunc, string queryName, TResult defaultValue = default(TResult))
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(LogOperations.DB_SELECT, _entityName, queryName);
            
            try
            {
                var result = queryFunc();
                stopwatch.Stop();
                
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, true, null);
                LogSlowQuery(queryName, stopwatch.ElapsedMilliseconds);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, LogOperations.DB_SELECT, false, null, ex.Message);
                throw;
            }
        }

        protected void LoggedExecute(Action executeAction, string operationName, string operationType = LogOperations.DB_UPDATE)
        {
            var stopwatch = Stopwatch.StartNew();
            var operationId = _logger.StartOperation(operationType, _entityName, operationName);
            
            try
            {
                executeAction();
                stopwatch.Stop();
                
                _logger.CompleteOperation(operationId, operationType, true);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.CompleteOperation(operationId, operationType, false, null, ex.Message);
                throw;
            }
        }
    }
}