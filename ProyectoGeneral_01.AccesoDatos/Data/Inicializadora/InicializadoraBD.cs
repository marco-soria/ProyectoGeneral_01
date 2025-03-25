using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoGeneral_01.Data;
using ProyectoGeneral_01.Models;
using ProyectoGeneral_01.Utilidades;

namespace ProyectoGeneral_01.AccesoDatos.Data.Inicializadora
{
    public class InicializadoraBD : IInicializadoraBD
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadoraBD(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (_db.Roles.Any(x => x.Name == Roles.Administrador))
            {
                return;
            }

            //Crear Roles
            _roleManager.CreateAsync(new IdentityRole(Roles.Administrador)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Usuario)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Cliente)).GetAwaiter().GetResult();

            //Crear usuario administrador
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "marco@gmail.com",
                Email = "marco@gmail.com",
                Nombre = "Marco",
                EmailConfirmed = true
            }, "Admin1@").GetAwaiter().GetResult();

            //Recuperamos el usuario creado
            ApplicationUser usuario =
                _db.ApplicationUser.Where(x => x.Email == "marco@gmail.com").FirstOrDefault();

            //Le asignamos el rol de administrador
            _userManager.AddToRoleAsync(usuario, Roles.Administrador).GetAwaiter().GetResult();
        }
    }
}
