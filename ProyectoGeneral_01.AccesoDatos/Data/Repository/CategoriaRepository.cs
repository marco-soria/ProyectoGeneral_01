using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Data;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(x => x.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;
            _db.SaveChanges();
        }
    }
}
