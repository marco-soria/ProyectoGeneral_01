using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGeneral_01.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Ingrese un nombre para la categoria")]
        [Display(Name = "Nombre de la categoria")]
        public string Nombre { get; set; }
       
        [Display(Name = "Orden de la visualizacion")]
        [Range(0,100, ErrorMessage = "El rango permitido es entre 1 y 100")]
        public int? Orden { get; set; }
    }
}
