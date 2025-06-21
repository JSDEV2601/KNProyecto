using KProyecto.EF;
using KProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KProyecto.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarPerfilUsuario()
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                var idUsuario = long.Parse(Session["IdUsuario"].ToString());
                var result = dbContext.TUsuario.FirstOrDefault(u => u.IdUsuario == idUsuario);

                if (result != null)
                {
                    var datos = new Usuario
                    {
                        Nombre = result.Nombre
                    };

                    return View(datos);
                }

                return View();

            }
        }
    }
}