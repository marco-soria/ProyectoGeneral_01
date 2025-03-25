using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Data;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void BloquearUsuario(string userId)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(x => x.Id == userId);
            usuarioDesdeDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }
        public void DesbloquearUsuario(string userId)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(x => x.Id == userId);
            usuarioDesdeDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
} 
