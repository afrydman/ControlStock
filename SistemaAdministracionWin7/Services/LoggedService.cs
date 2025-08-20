using System;
using System.Collections.Generic;
using System.Diagnostics;
using DTO.BusinessEntities;
using Persistence.LogService;
using Repository;

namespace Services
{
    /// <summary>
    /// Base service class with enhanced logging capabilities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class LoggedService<T> where T : GenericObject
    {
        protected readonly BusinessLogger _businessLogger;
        protected readonly DatabaseLogger _databaseLogger;
        protected readonly string _entityName;
        protected readonly IGenericRepository<T> _repository;

        protected LoggedService(string entityName, IGenericRepository<T> repository)
        {
            _entityName = entityName;
            _repository = repository;
            _businessLogger = LoggerFactory.GetBusinessLogger(string.Format("{0}Service", entityName));
            _databaseLogger = LoggerFactory.GetDatabaseLogger(string.Format("{0}Service", entityName));
        }

        protected void SetContext(string user, string sessionId)
        {
            _businessLogger.SetContext(user, sessionId);
            _databaseLogger.SetContext(user, sessionId);
        }

        public virtual bool Insert(T entity)
        {
            return LoggedBusinessOperation(() =>
            {
                // Pre-insert validation
                ValidateForInsert(entity);
                
                // Execute insert
                var success = _repository.Insert(entity);
                
                if (success)
                {
                    // Post-insert operations
                    OnEntityInserted(entity);
                    LogBusinessEvent("CREATE", entity, "Entity created successfully");
                }
                
                return success;
            }, "INSERT");
        }

        public virtual bool Update(T entity)
        {
            return LoggedBusinessOperation(() =>
            {
                // Get existing entity for comparison
                var existingEntity = _repository.GetByID(entity.ID);
                
                // Pre-update validation
                ValidateForUpdate(entity, existingEntity);
                
                // Execute update
                var success = _repository.Update(entity);
                
                if (success)
                {
                    // Post-update operations
                    OnEntityUpdated(entity, existingEntity);
                    LogBusinessEvent("UPDATE", entity, "Entity updated successfully");
                }
                
                return success;
            }, "UPDATE");
        }

        public virtual bool Delete(Guid id)
        {
            return LoggedBusinessOperation(() =>
            {
                // Get entity before deletion for logging
                var entity = _repository.GetByID(id);
                
                if (entity == null)
                {
                    LogBusinessEvent("DELETE", null, string.Format("Entity with ID {0} not found for deletion", id));
                    return false;
                }
                
                // Pre-delete validation
                ValidateForDelete(entity);
                
                // Execute delete (disable)
                var success = _repository.Disable(id);
                
                if (success)
                {
                    // Post-delete operations
                    OnEntityDeleted(entity);
                    LogBusinessEvent("DELETE", entity, "Entity deleted successfully");
                }
                
                return success;
            }, "DELETE");
        }

        public virtual T GetByID(Guid id)
        {
            return LoggedBusinessOperation(() =>
            {
                var entity = _repository.GetByID(id);
                
                if (entity == null)
                {
                    LogBusinessEvent("READ", null, string.Format("Entity with ID {0} not found", id));
                }
                
                return entity;
            }, "GET_BY_ID");
        }

        public virtual List<T> GetAll()
        {
            return LoggedBusinessOperation(() =>
            {
                var entities = _repository.GetAll();
                LogBusinessEvent("READ_ALL", null, string.Format("Retrieved {0} entities", entities.Count));
                return entities;
            }, "GET_ALL");
        }

        // Business validation methods (to be overridden by derived classes)
        protected virtual void ValidateForInsert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity.ID == Guid.Empty)
                entity.ID = Guid.NewGuid();
        }

        protected virtual void ValidateForUpdate(T entity, T existingEntity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (existingEntity == null)
                throw new InvalidOperationException(string.Format("{0} with ID {1} not found for update", _entityName, entity.ID));
        }

        protected virtual void ValidateForDelete(T entity)
        {
            // Override in derived classes for specific validation
        }

        // Event handlers (to be overridden by derived classes)
        protected virtual void OnEntityInserted(T entity)
        {
            // Override in derived classes
        }

        protected virtual void OnEntityUpdated(T newEntity, T oldEntity)
        {
            // Override in derived classes
        }

        protected virtual void OnEntityDeleted(T entity)
        {
            // Override in derived classes
        }

        // Logging helpers
        protected void LogBusinessEvent(string operation, T entity, string details)
        {
            var entityInfo = entity?.ToString() ?? "Unknown";
            _businessLogger.LogProductOperation(operation, entity?.ID.ToString() ?? "N/A", entityInfo, true, details);
        }

        protected TResult LoggedBusinessOperation<TResult>(Func<TResult> operation, string operationName, TResult defaultValue = default(TResult))
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _businessLogger.LogInfo(string.Format("Starting {0} operation on {1}", operationName, _entityName), operationName);
                
                var result = operation();
                
                stopwatch.Stop();
                _businessLogger.LogInfo(string.Format("Completed {0} operation on {1} in {2}ms", operationName, _entityName, stopwatch.ElapsedMilliseconds), operationName);
                
                // Log performance warnings
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    _businessLogger.LogWarning(string.Format("Slow business operation: {0} on {1} took {2}ms", operationName, _entityName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _businessLogger.LogError(string.Format("Failed {0} operation on {1} after {2}ms", operationName, _entityName, stopwatch.ElapsedMilliseconds), ex, operationName);
                throw;
            }
        }

        protected void LoggedBusinessOperation(Action operation, string operationName)
        {
            LoggedBusinessOperation(() => { operation(); return true; }, operationName);
        }

        // Business rules validation with logging
        protected bool ValidateBusinessRule(string ruleName, Func<bool> ruleCheck, string details = null)
        {
            try
            {
                var passed = ruleCheck();
                _businessLogger.LogBusinessRule(ruleName, _entityName, passed, details);
                return passed;
            }
            catch (Exception ex)
            {
                _businessLogger.LogError(string.Format("Business rule validation failed: {0}", ruleName), ex, "VALIDATION");
                return false;
            }
        }

        // Custom query logging
        protected TResult ExecuteCustomQuery<TResult>(Func<TResult> queryFunc, string queryName, TResult defaultValue = default(TResult))
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _databaseLogger.LogInfo(string.Format("Executing custom query: {0}", queryName), LogOperations.DB_SELECT);
                
                var result = queryFunc();
                
                stopwatch.Stop();
                
                // Log slow queries
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _databaseLogger.LogWarning(string.Format("Slow query: {0} took {1}ms", queryName, stopwatch.ElapsedMilliseconds), "PERFORMANCE");
                }
                else
                {
                    _databaseLogger.LogDebug(string.Format("Query completed: {0} in {1}ms", queryName, stopwatch.ElapsedMilliseconds), LogOperations.DB_SELECT);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _databaseLogger.LogError(string.Format("Query failed: {0} after {1}ms", queryName, stopwatch.ElapsedMilliseconds), ex, LogOperations.DB_SELECT);
                return defaultValue;
            }
        }

        // Bulk operations with logging
        protected bool BulkInsert(List<T> entities)
        {
            return LoggedBusinessOperation(() =>
            {
                var successful = 0;
                var failed = 0;
                
                foreach (var entity in entities)
                {
                    try
                    {
                        if (_repository.Insert(entity))
                            successful++;
                        else
                            failed++;
                    }
                    catch (Exception ex)
                    {
                        _businessLogger.LogError(string.Format("Bulk insert failed for entity {0}", entity.ID), ex, "BULK_INSERT");
                        failed++;
                    }
                }
                
                _businessLogger.LogInfo(string.Format("Bulk insert completed: {0} successful, {1} failed", successful, failed), "BULK_INSERT");
                return failed == 0;
            }, "BULK_INSERT");
        }
    }
}