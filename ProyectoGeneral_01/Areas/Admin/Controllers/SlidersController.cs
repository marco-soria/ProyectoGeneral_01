using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _iContenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IContenedorTrabajo iContenedorTrabajo, IWebHostEnvironment webhostEnvironment)
        {
            _iContenedorTrabajo = iContenedorTrabajo;
            _webHostEnvironment = webhostEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
           string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString();
                var upload = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                using(var fileStreams = new FileStream(Path.Combine(upload, nombreArchivo + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                _iContenedorTrabajo.ISliderRepository.Add(slider);
                _iContenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }


        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var slider = _iContenedorTrabajo.ISliderRepository.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            var oSlider = _iContenedorTrabajo.ISliderRepository.Get(slider.Id);

            if(archivos.Count() > 0)
            {
                //Crear una nueva imagen
                string nombreArchivo = Guid.NewGuid().ToString();
                var subida = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                var extension = Path.GetExtension(archivos[0].FileName);

                var rutaImagen = Path.Combine(rutaPrincipal, oSlider.UrlImagen.TrimStart('\\'));
                if(System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }

                //subir la imagen
                using (var fileStreams = new FileStream(Path.Combine(subida, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                _iContenedorTrabajo.ISliderRepository.Update(slider);
                _iContenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {  //en caso no cambiemos la imagen
                slider.UrlImagen = oSlider.UrlImagen;
            }

            _iContenedorTrabajo.ISliderRepository.Update(slider);
            _iContenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));

        }



        #endregion

        #region Delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oSlider = _iContenedorTrabajo.ISliderRepository.Get(id);
            string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaPrincipal, oSlider.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (oSlider == null)
            {
                return Json(new { success = false, message = "Error al eliminar el slider" });
            }


            _iContenedorTrabajo.ISliderRepository.Remove(oSlider);
            _iContenedorTrabajo.Save();

            return Json(new { success = true, message = "Slider eliminado con exito" });

        }
        #endregion

        #region Llamadas a la Api



        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _iContenedorTrabajo.ISliderRepository.GetAll() });
        }
        #endregion
    }
}
    