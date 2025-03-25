using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<ApplicationUser>
    {
        void BloquearUsuario(string userId);
        void DesbloquearUsuario(string userId);
    }
}
