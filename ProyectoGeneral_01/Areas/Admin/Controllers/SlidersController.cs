using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;

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
