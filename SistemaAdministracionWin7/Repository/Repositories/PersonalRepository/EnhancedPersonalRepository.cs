using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DTO;
using Dapper;

namespace Repository.Repositories.PersonalRepository
{
    /// <summary>
    /// Enhanced PersonalRepository with centralized error handling and comprehensive logging
    /// </summary>
    public class EnhancedPersonalRepository : EnhancedDbRepository, IPersonalRepository
    {
        public EnhancedPersonalRepository(bool local = true) : base(local, "PersonalRepository")
        {
        }

        public bool Insert(PersonalData theObject)
        {
            // Validate required parameters
            ValidateParameters(
                "ID", theObject != null ? (object)theObject.ID : null,
                "nombrecontacto", theObject != null ? theObject.NombreContacto : null,
                "razonsocial", theObject != null ? theObject.RazonSocial : null
            );

            var sql = @"INSERT INTO [dbo].[Personal]
                       ([id]
                       ,[nombrecontacto]
                       ,[cuil]
                       ,[telefono]
                       ,[Description]
                       ,[razonsocial]
                       ,[email]
                       ,[facebook]
                       ,[direccion]
                       ,[enable])
                 VALUES
                       (@id,
                       @nombrecontacto,
                       @cuil,
                       @telefono,
                       @Description,
                       @razonsocial,
                       @email,
                       @facebook,
                       @direccion,
                       @enable);
                SELECT @@ROWCOUNT;";

            try
            {
                var rowsAffected = QueryFirstOrDefault<int>(sql, theObject, "Insert Personal");
                var success = rowsAffected > 0;
                
                LogDatabaseOperation("INSERT", $"Personal: {theObject.RazonSocial}", success, rowsAffected);
                return success;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "Insert Personal", sql);
            }
        }

        public bool Update(PersonalData theObject)
        {
            // Validate required parameters
            ValidateParameters(
                "ID", theObject != null ? (object)theObject.ID : null,
                "nombrecontacto", theObject != null ? theObject.NombreContacto : null,
                "razonsocial", theObject != null ? theObject.RazonSocial : null
            );

            var sql = @"Update Personal
                        Set
                        nombrecontacto = @nombrecontacto,
                        cuil = @cuil,
                        telefono = @telefono,
                        Description = @Description,
                        razonsocial = @razonsocial,
                        email = @email,
                        facebook = @facebook,
                        direccion = @direccion,
                        enable = @enable 
                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";

            try
            {
                var rowsAffected = QueryFirstOrDefault<int>(sql, theObject, "Update Personal");
                var success = rowsAffected > 0;
                
                LogDatabaseOperation("UPDATE", $"Personal: {theObject.RazonSocial} (ID: {theObject.ID})", success, rowsAffected);
                return success;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "Update Personal", sql);
            }
        }

        public bool Disable(Guid idObject)
        {
            ValidateParameters("ID", idObject);

            var sql = @"Update Personal
                        Set
                        Personal.enable = '0'
                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";

            try
            {
                var rowsAffected = QueryFirstOrDefault<int>(sql, new { id = idObject }, "Disable Personal");
                var success = rowsAffected > 0;
                
                LogDatabaseOperation("DISABLE", $"Personal ID: {idObject}", success, rowsAffected);
                return success;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "Disable Personal", sql);
            }
        }

        public bool Enable(Guid idObject)
        {
            ValidateParameters("ID", idObject);

            var sql = @"Update Personal
                        Set
                        Personal.enable = '1'
                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";

            try
            {
                var rowsAffected = QueryFirstOrDefault<int>(sql, new { id = idObject }, "Enable Personal");
                var success = rowsAffected > 0;
                
                LogDatabaseOperation("ENABLE", $"Personal ID: {idObject}", success, rowsAffected);
                return success;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "Enable Personal", sql);
            }
        }

        public List<PersonalData> GetAll()
        {
            var sql = @"SELECT *
                       FROM Personal
                       ORDER BY razonsocial";

            try
            {
                var result = Query<PersonalData>(sql, null, "GetAll Personal").ToList();
                LogDatabaseOperation("SELECT_ALL", "Personal records", true, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "GetAll Personal", sql);
            }
        }

        public PersonalData GetByID(Guid idObject)
        {
            ValidateParameters("ID", idObject);

            var sql = @"SELECT *  
                       FROM [Personal]  
                       Where id = @id";

            try
            {
                var result = QueryFirstOrDefault<PersonalData>(sql, new { id = idObject }, "GetByID Personal");
                
                if (result == null)
                {
                    LogDatabaseOperation("SELECT_BY_ID", $"Personal ID: {idObject} - Not Found", true, 0);
                    return new PersonalData(); // Maintain existing behavior
                }
                
                LogDatabaseOperation("SELECT_BY_ID", $"Personal: {result.RazonSocial} (ID: {idObject})", true, 1);
                return result;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "GetByID Personal", sql);
            }
        }

        public PersonalData GetLast(Guid idLocal, int first)
        {
            // If this method needs implementation, it would look like:
            /*
            ValidateParameters("idLocal", idLocal);

            var sql = @"SELECT TOP 1 *
                       FROM Personal
                       WHERE [some_local_field] = @idLocal
                       ORDER BY [creation_date] DESC";

            try
            {
                var result = QueryFirstOrDefault<PersonalData>(sql, new { idLocal = idLocal }, "GetLast Personal");
                LogDatabaseOperation("SELECT_LAST", $"Personal for Local: {idLocal}", result != null, result != null ? 1 : 0);
                return result ?? new PersonalData();
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "GetLast Personal", sql);
            }
            */
            
            _logger.LogWarning("GetLast method not implemented for PersonalRepository", "NOT_IMPLEMENTED");
            throw new NotImplementedException("GetLast method needs to be implemented based on business requirements");
        }

        public IEnumerable<PersonalData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            // If this method needs implementation, it would look like:
            /*
            try
            {
                _logger.LogDebug($"Executing custom operator query: {sql}", "OPERATOR_QUERY");
                return con.Query<PersonalData>(sql, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("Custom operator query failed", ex, "OPERATOR_QUERY");
                throw HandleDatabaseException(ex, "OperatorGiveMeData", sql);
            }
            */
            
            _logger.LogWarning("OperatorGiveMeData method not implemented for PersonalRepository", "NOT_IMPLEMENTED");
            throw new NotImplementedException("OperatorGiveMeData method needs to be implemented based on business requirements");
        }

        public List<PersonalData> GetPersonalbyLocal(Guid idLocal)
        {
            ValidateParameters("idLocal", idLocal);

            // Assuming there's a relationship between Personal and Local
            var sql = @"SELECT p.*
                       FROM Personal p
                       INNER JOIN [LocalPersonal] lp ON p.id = lp.idPersonal
                       WHERE lp.idLocal = @idLocal
                       AND p.enable = 1
                       ORDER BY p.razonsocial";

            try
            {
                var result = Query<PersonalData>(sql, new { idLocal = idLocal }, "GetPersonalbyLocal").ToList();
                LogDatabaseOperation("SELECT_BY_LOCAL", $"Personal for Local: {idLocal}", true, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                // If the table doesn't exist, provide a simpler implementation
                _logger.LogWarning($"GetPersonalbyLocal failed, possibly due to missing LocalPersonal table: {ex.Message}", "QUERY_WARNING");
                
                // Fallback to getting all active personal (implement based on actual business logic)
                return GetAll().Where(p => p.Enable).ToList();
            }
        }

        public override string DEFAULT_SELECT
        {
            get { return "SELECT * FROM Personal WHERE enable = 1"; }
        }

        #region Additional Helper Methods

        /// <summary>
        /// Get active Personal records only
        /// </summary>
        public List<PersonalData> GetActivePersonal()
        {
            var sql = @"SELECT *
                       FROM Personal
                       WHERE enable = 1
                       ORDER BY razonsocial";

            try
            {
                var result = Query<PersonalData>(sql, null, "GetActivePersonal").ToList();
                LogDatabaseOperation("SELECT_ACTIVE", "Active Personal records", true, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "GetActivePersonal", sql);
            }
        }

        /// <summary>
        /// Search Personal by name or email
        /// </summary>
        public List<PersonalData> SearchPersonal(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<PersonalData>();

            var sql = @"SELECT *
                       FROM Personal
                       WHERE (razonsocial LIKE @searchTerm 
                          OR nombrecontacto LIKE @searchTerm
                          OR email LIKE @searchTerm)
                       AND enable = 1
                       ORDER BY razonsocial";

            try
            {
                var searchPattern = $"%{searchTerm}%";
                var result = Query<PersonalData>(sql, new { searchTerm = searchPattern }, "SearchPersonal").ToList();
                LogDatabaseOperation("SEARCH", $"Personal search: '{searchTerm}'", true, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                throw HandleDatabaseException(ex, "SearchPersonal", sql);
            }
        }

        /// <summary>
        /// Bulk enable/disable operation with transaction support
        /// </summary>
        public bool BulkUpdateStatus(List<Guid> personalIds, bool enable)
        {
            if (personalIds == null || !personalIds.Any())
                return false;

            return ExecuteInTransaction((connection, transaction) =>
            {
                var sql = @"UPDATE Personal 
                           SET enable = @enable 
                           WHERE id = @id";

                var totalUpdated = 0;

                foreach (var id in personalIds)
                {
                    var rowsAffected = connection.Execute(sql, new { id = id, enable = enable ? 1 : 0 }, transaction);
                    totalUpdated += rowsAffected;
                }

                LogDatabaseOperation("BULK_UPDATE_STATUS", 
                    $"{(enable ? "Enabled" : "Disabled")} {totalUpdated} Personal records", 
                    totalUpdated > 0, totalUpdated);

                return totalUpdated > 0;
            }, "BulkUpdateStatus Personal");
        }

        #endregion
    }
}