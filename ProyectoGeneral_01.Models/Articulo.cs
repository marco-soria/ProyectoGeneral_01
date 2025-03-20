using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGeneral_01.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del articulo es obligatorio")]
        [Display(Name = "Nombre del articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion del articulo es obligatoria")]
        public string Descripcion { get; set; }

        [Display(Name ="Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }


        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
