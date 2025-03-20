using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using ProyectoGeneral_01.Models;
using ProyectoGeneral_01.Models.ViewModel;

namespace ProyectoGeneral_01.Areas.Cliente.Controllers;

[Area("Cliente")]
public class HomeController : Controller
{
    
    private readonly IContenedorTrabajo _iContenedorTrabajo;

    public HomeController(IContenedorTrabajo iContenedorTrabajo)
    {
        _iContenedorTrabajo = iContenedorTrabajo;
    }
    [HttpGet]
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel()
        {
            listSliders = _iContenedorTrabajo.ISliderRepository.GetAll(),
            listArticulos = _iContenedorTrabajo.IArticuloRepository.GetAll(includeProperties: "Categoria")
        };

        ViewBag.isHome = true;

        return View(homeViewModel);
    }

    [HttpGet]
    public ActionResult Detalle(int id)
    {
        var articulo = _iContenedorTrabajo.IArticuloRepository.GetFirstOrDefault(includeProperties: "Categoria", filter: a => a.Id == id);
        return View(articulo);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
