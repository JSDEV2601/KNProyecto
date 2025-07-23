using KProyecto.EF;
using KProyecto.Models;
using KProyecto.Services;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KProyecto.Controllers
{
    public class HomeController : Controller
    {
        readonly Utilitarios service = new Utilitarios();

        #region Index

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Autenticacion autenticacion)
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                //var result = dbContext.TUsuario.FirstOrDefault(u => u.Correo == autenticacion.Correo
                //                                                 && u.Contrasenna == autenticacion.Contrasenna);

                var result = dbContext.ValidarInicioSesion(
                    autenticacion.Correo,
                    autenticacion.Contrasenna).FirstOrDefault();

                if (result != null)
                {
                    Session["IdUsuario"] = result.IdUsuario;
                    Session["Nombre"] = result.Nombre;
                    Session["IdRol"] = result.IdRol;
                    Session["DescripcionRol"] = result.DescripcionRol;
                    return RedirectToAction("Principal", "Home");
                }

                ViewBag.Mensaje = "No se pudo validar su información";
                return View();
            }
        }

        #endregion

        #region Registro

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Autenticacion autenticacion)
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                //dbContext.TUsuario.Add(new TUsuario
                //{
                //    Identificacion = autenticacion.Identificacion,
                //    Nombre = autenticacion.Nombre,
                //    Correo = autenticacion.Correo,
                //    Contrasenna = autenticacion.Contrasenna
                //});

                //var result = dbContext.SaveChanges();

                var result = dbContext.RegistroUsuario(
                    autenticacion.Identificacion,
                    autenticacion.Nombre,
                    autenticacion.Correo,
                    autenticacion.Contrasenna);

                if (result > 0)
                    return RedirectToAction("Index", "Home");

                ViewBag.Mensaje = "No se pudo registrar su información";
                return View();
            }
        }

        #endregion

        #region RecuperarContrasenna

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(Autenticacion autenticacion)
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                var result = dbContext.TUsuario.FirstOrDefault(u => u.Correo == autenticacion.Correo);

                if (result != null)
                {
                    var Contrasenna = service.GenerarPassword();

                    result.Contrasenna = Contrasenna;
                    dbContext.SaveChanges();

                    StringBuilder mensaje = new StringBuilder();

                    mensaje.Append("Estimado " + result.Nombre + "<br>");
                    mensaje.Append("Se ha generado una solicitud de recuperación de contraseña a su nombre.<br><br>");
                    mensaje.Append("Su contraseña temporal es: " + Contrasenna + "<br><br>");
                    mensaje.Append("Procure realizar el cambio de su contraseña en cuanto ingrese al sistema.<br>");
                    mensaje.Append("Muchas gracias.");

                    if (service.EnviarCorreo(result.Correo, mensaje.ToString(), "Solicitud de acceso"))
                        return RedirectToAction("Index", "Home");

                    ViewBag.Mensaje = "No se pudo realizar la notificación de su acceso al sistema";
                    return View();
                }

                ViewBag.Mensaje = "No se pudo recuperar su acceso al sistema";
                return View();
            }
        }

        #endregion

        [FiltroSesion]
        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }

        [FiltroSesion]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}