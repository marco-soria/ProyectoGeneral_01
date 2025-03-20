using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Models;
using ProyectoGeneral_01.Models.ViewModel;

namespace ProyectoGeneral_01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _iContenedorTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _iContenedorTrabajo = contenedorTrabajo;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            ArticuloCategoriaViewModel articuloCategoriaViewModel = new ArticuloCategoriaViewModel()
            {
                Articulo = new Articulo(),
                ListaCategorias = _iContenedorTrabajo.ICategoriaRepository.GetListaCategorias()
            };

            articuloCategoriaViewModel.ListaCategorias = _iContenedorTrabajo.ICategoriaRepository.GetListaCategorias();
            return View(articuloCategoriaViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ArticuloCategoriaViewModel articuloCategoriaViewModel)
        {
            string mainRoute = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (articuloCategoriaViewModel.Articulo.Id == 0 && files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\articulos");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                articuloCategoriaViewModel.Articulo.UrlImagen = @"\imagenes\articulos\" + nameFile + extension;
                articuloCategoriaViewModel.Articulo.FechaCreacion = DateTime.Now;

                _iContenedorTrabajo.IArticuloRepository.Add(articuloCategoriaViewModel.Articulo);
                _iContenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            articuloCategoriaViewModel.ListaCategorias = _iContenedorTrabajo.ICategoriaRepository.GetListaCategorias();
            return View(articuloCategoriaViewModel);

        }

        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        { 
            ArticuloCategoriaViewModel articuloCategoriaViewModel = new ArticuloCategoriaViewModel()
            {
                Articulo = new Articulo(),
                ListaCategorias = _iContenedorTrabajo.ICategoriaRepository.GetListaCategorias()
            };


            if (id != null)
            {
                articuloCategoriaViewModel.Articulo = _iContenedorTrabajo.IArticuloRepository.Get((int)id);
            }
            return View(articuloCategoriaViewModel);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ArticuloCategoriaViewModel articuloCategoriaViewModel)
        {
            string mainRoute = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var articuloDesdeDb = _iContenedorTrabajo.IArticuloRepository.Get(articuloCategoriaViewModel.Articulo.Id);

            //en el caso de que se suba una imagen
            if (files.Count > 0)
            {
                var fileNames = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\articulos");
                var extension = Path.GetExtension(files[0].FileName);

                //Comprobamos si la imagen ya existe
                var rutaImagenAntigua = Path.Combine(mainRoute, articuloDesdeDb.UrlImagen.TrimStart('\\'));
                if (System.IO.File.Exists(rutaImagenAntigua))
                {
                    System.IO.File.Delete(rutaImagenAntigua);
                }

                //nuevamente subimos el archivo
                using (var fileStreams = new FileStream(Path.Combine(upload, fileNames + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                articuloCategoriaViewModel.Articulo.UrlImagen = @"\imagenes\articulos\" + fileNames + extension;
                articuloCategoriaViewModel.Articulo.FechaCreacion = DateTime.Now;

                _iContenedorTrabajo.IArticuloRepository.Update(articuloCategoriaViewModel.Articulo);
                _iContenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));

            }
            else
            {
                //cuando la imagen existe y esta se conserva
                articuloCategoriaViewModel.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
            }

            _iContenedorTrabajo.IArticuloRepository.Update(articuloCategoriaViewModel.Articulo);
            _iContenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));

        }


        #endregion


        #region Delete

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oArticulo = _iContenedorTrabajo.IArticuloRepository.Get(id);
            var mainRoute = _hostEnvironment.WebRootPath;
            var rootImage = Path.Combine(mainRoute, oArticulo.UrlImagen.TrimStart('\\'));
            if(System.IO.File.Exists(rootImage))
            {
                System.IO.File.Delete(rootImage);
            }

            if(oArticulo == null)
            {
                return Json(new { success = false, message = "Error al borrar el articulo" });
            }

            _iContenedorTrabajo.IArticuloRepository.Remove(oArticulo);
            _iContenedorTrabajo.Save();
            return Json(new { success = true, message = "Articulo borrado con exito" });
        }

        #endregion

        #region LLamada a la API


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _iContenedorTrabajo.IArticuloRepository.GetAll(includeProperties: "Categoria") });
        }
        #endregion
    }
}
