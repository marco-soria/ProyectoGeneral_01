using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoGeneral_01.Models.ViewModel
{
    public class ArticuloCategoriaViewModel
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
