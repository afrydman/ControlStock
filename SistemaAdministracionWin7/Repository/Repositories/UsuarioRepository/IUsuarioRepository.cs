using DTO.BusinessEntities;

namespace Repository.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        usuarioData GetUsuarioByUserName(string username);

    }
}
