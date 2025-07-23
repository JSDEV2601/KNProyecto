using KProyecto.EF;
using KProyecto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KProyecto.Controllers
{
    [FiltroAdministrador]
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarProductos()
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                var datos = new List<Producto>();

                //var result = dbContext.TProducto.ToList();
                var result = dbContext.ConsultarProductos().ToList();

                //foreach (var item in result)
                //{
                //    var producto = new Producto
                //    {
                //        Nombre = item.Nombre
                //    };
                //    datos.Add(producto);
                //}

                return View(result);
            }
        }

        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProducto(Producto producto, HttpPostedFileBase ImagenProducto)
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                var tabla = new TProducto();
                tabla.IdProducto = 0;
                tabla.Nombre = producto.Nombre;
                tabla.Descripcion = producto.Descripcion;
                tabla.Cantidad = producto.Cantidad;
                tabla.Precio = producto.Precio;
                tabla.Estado = true;
                tabla.Imagen = string.Empty;

                dbContext.TProducto.Add(tabla);
                var result = dbContext.SaveChanges();

                //var result = dbContext.RegistroProducto(
                //    producto.Nombre,
                //    producto.Descripcion,
                //    producto.Cantidad,
                //    producto.Precio,
                //    producto.Imagen).FirstOrDefault();

                if (result > 0)
                {
                    string extension = Path.GetExtension(ImagenProducto.FileName);
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Productos\\" + tabla.IdProducto + extension;
                    ImagenProducto.SaveAs(ruta);

                    tabla.Imagen = "/Productos/" + tabla.IdProducto + extension;
                    dbContext.SaveChanges();

                    return RedirectToAction("ConsultarProductos", "Producto");
                }

                ViewBag.Mensaje = "No se pudo registrar el producto";
                return View();
            }


        }

        [HttpGet]
        public ActionResult ActualizarProducto(long id)
        {
            return View();
        }

        //httppost


        [HttpPost]
        public ActionResult CambiarEstadoProducto(Producto producto)
        {
            using (var dbContext = new KNDataBaseEntities())
            {
                var result = dbContext.TProducto.FirstOrDefault(u => u.IdProducto == producto.IdProducto);

                if (result != null)
                {
                    result.Estado = producto.Estado;
                    var update = dbContext.SaveChanges();

                    if (update > 0)
                        return RedirectToAction("ConsultarProductos", "Producto");
                }

                ViewBag.Mensaje = "No se pudo actualizar el estado del producto producto";
                return View();
            }

            
        }
    }
}