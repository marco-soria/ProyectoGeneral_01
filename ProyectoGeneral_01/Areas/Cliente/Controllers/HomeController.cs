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
    //[HttpGet]
    //public IActionResult Index()
    //{
    //    HomeViewModel homeViewModel = new HomeViewModel()
    //    {
    //        listSliders = _iContenedorTrabajo.ISliderRepository.GetAll(),
    //        listArticulos = _iContenedorTrabajo.IArticuloRepository.GetAll(includeProperties: "Categoria")
    //    };

    //    ViewBag.isHome = true;

    //    return View(homeViewModel);
    //}

    //Pagina Index con paginacion
    [HttpGet]
    public IActionResult Index (int page =1, int pageSize = 6)
    {
        var articulos = _iContenedorTrabajo.IArticuloRepository.AsQueryable();
        var paginaEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize);

        HomeViewModel homeViewModel = new HomeViewModel()
        {
            listSliders = _iContenedorTrabajo.ISliderRepository.GetAll(),
            listArticulos = paginaEntries.ToList(),
            PageIndex = page,
            TotalPages = (int)Math.Ceiling(articulos.Count() / (double)pageSize)
        };

        ViewBag.IsHome = true;

        return View(homeViewModel);
    }

    //Implementación de la búsqueda
    [HttpGet]
    public IActionResult ResultadosBusqueda(string searchString, int page = 1, int pageSize = 10)
    {
        var articulos = _iContenedorTrabajo.IArticuloRepository.AsQueryable();
        if(!String.IsNullOrEmpty(searchString))
        {
            articulos = articulos.Where(a => a.Nombre.ToLower().Contains(searchString.ToLower()) ||
            a.Descripcion.ToLower().Contains(searchString.ToLower()));
        }

        //Contar los elementos despues de aplicar el filtro
        var totalArticulos = articulos.Count();

        //Paginar los resultados
        var pageEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        //crear el modelo de la vista
        ListaPaginada<Articulo> articulosPaginados = 
            new ListaPaginada<Articulo>(pageEntries.ToList(), totalArticulos, page, pageSize, searchString);

        return View(articulosPaginados);
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
