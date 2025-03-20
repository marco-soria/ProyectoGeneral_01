using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGeneral_01.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            ICategoriaRepository = new CategoriaRepository(_db);
            IArticuloRepository = new ArticuloRepository(_db);
            ISliderRepository = new SliderRepository(_db);
        }
        public ICategoriaRepository ICategoriaRepository { get; private set; }
        public IArticuloRepository IArticuloRepository { get; private set; }

        public ISliderRepository ISliderRepository { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
