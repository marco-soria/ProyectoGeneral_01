using Microsoft.EntityFrameworkCore;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Data;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Articulo> AsQueryable()
        {
            return _db.Set<Articulo>().AsQueryable();
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);

            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;
            _db.SaveChanges();
        }
    }
}
