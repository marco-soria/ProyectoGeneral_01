﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGeneral_01.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La direccion es obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatorio")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El pais es obligatorio")]
        public string Pais { get; set; }

    }
}
