using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Models;

namespace ProyectoGeneral_01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _iContenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _iContenedorTrabajo = contenedorTrabajo;
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
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _iContenedorTrabajo.ICategoriaRepository.Add(categoria);
                _iContenedorTrabajo.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _iContenedorTrabajo.ICategoriaRepository.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _iContenedorTrabajo.ICategoriaRepository.Update(categoria);
                _iContenedorTrabajo.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        #endregion
        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _iContenedorTrabajo.ICategoriaRepository.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objDesdeDb = _iContenedorTrabajo.ICategoriaRepository.Get(id);
            if (objDesdeDb == null)
            {
                return Json(new { success = false, message = "Error al borrar la categoria" });
            }
            _iContenedorTrabajo.ICategoriaRepository.Remove(objDesdeDb);
            _iContenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoria borrada con exito" });

        }
        #endregion
    }
}