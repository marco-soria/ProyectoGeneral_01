using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);

        IEnumerable<SelectListItem> GetListaCategorias();
    }
}
