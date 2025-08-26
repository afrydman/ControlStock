using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repository.Repositories
{

    public interface ITransferRepository : IGenericRepository<FileTransferData>, IGenericGetters<FileTransferData> { 
    
    
    }
   public  class FileTransferRepository : DbRepository,ITransferRepository
    {

        public override string DEFAULT_SELECT => @" [FileTransfer].* 
	
  FROM [dbo].[FileTransfer]
 
   ";


        public FileTransferRepository(bool local = true) : base(local)
        {
        }
        public bool Disable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<FileTransferData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT;
                IEnumerable<FileTransferData> resultado = con.Query<FileTransferData>(sql
                       
                       );
                return resultado.ToList();
            }
        }

        public FileTransferData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public FileTransferData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public bool Insert(FileTransferData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[FileTransfer]
                                ([ID],[Description],[Enable],[Date],[Error],[Completed],[ToS3CompleteDir],[LocalFileName],[FromLocalID],[ToLocalID])
	                            Values
		                        (@id,@Description,@Enable,@Date,@Error,@Completed,@ToS3CompleteDir,@LocalFileName,@FromLocalID,@ToLocalID)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    theObject.ID,
                    theObject.Description,
                    theObject.Enable,
                    theObject.Date,
                    theObject.Error,
                    theObject.Completed,
                    theObject.ToS3CompleteDir,
                    theObject.LocalFileName,
                    theObject.FromLocalID,
                    theObject.ToLocalID


                }) > 0;

            }
        }

        public IEnumerable<FileTransferData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(FileTransferData theObject)
        {
            throw new NotImplementedException();
        }

        public List<FileTransferData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            throw new NotImplementedException();
        }

        public List<FileTransferData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public List<FileTransferData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }
    }
}
