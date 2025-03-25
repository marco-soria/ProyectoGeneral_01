using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGeneral_01.Models
{
    public class ListaPaginada<T> : List<T>
    {
        public int PageIndex { get; private set; } //Pagina actual
        public int TotalPages { get; private set; } //Total de paginas
        public string SearchString { get; private set; } //Texto de busqueda

        //List<t> -> la lista de elementos en la pagina actual
        //count -> Numero total de elementos en la coleccion original
        //pageSize -> Cantidad de elementos por pagina
        public ListaPaginada(List<T> items, int count, int pageIndex, int pageSize, string searchString)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SearchString = searchString;
            this.AddRange(items);
        }

        //Retorna un true si hay una pagina anterior
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        //Retorna un true si hay una pagina siguiente
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

    }
}
