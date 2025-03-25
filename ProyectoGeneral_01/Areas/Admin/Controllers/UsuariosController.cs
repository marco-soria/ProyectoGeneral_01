using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository;
using System.Security.Claims;

namespace ProyectoGeneral_01.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _iContenedorTrabajo;

        public UsuariosController(IContenedorTrabajo iContenedorTrabajo)
        {
            _iContenedorTrabajo = iContenedorTrabajo;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            //var listUsuario = _iContenedorTrabajo.IUsuarioRepository.GetAll();
            //return View(listUsuario);

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var user = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_iContenedorTrabajo.IUsuarioRepository.GetAll(x => x.Id != user.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _iContenedorTrabajo.IUsuarioRepository.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _iContenedorTrabajo.IUsuarioRepository.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
