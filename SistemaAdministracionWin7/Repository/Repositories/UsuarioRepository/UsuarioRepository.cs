using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.UsuarioRepository
{
    public class UsuarioRepository : DbRepository,IUsuarioRepository
    {
        public UsuarioRepository(bool local=true) : base(local)
        {
        }

        public usuarioData GetUsuarioByUserName(string username)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<usuarioData>("Select * From usuario Where usuario = @username ", new { username = username }) ?? new usuarioData();
            }
        }


        public override string DEFAULT_SELECT
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
