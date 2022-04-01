using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PL_MVC.Controllers
{
    public class AseguradoraController : Controller
    {
        // GET: Aseguradora
        public ActionResult GetAll()
        {
            ML.Result resultAseguradora = BL.Aseguradora.GetAllEF();
            if (resultAseguradora.Correct)
            {
                ML.Aseguradora aseguradora = new ML.Aseguradora();
                aseguradora.Aseguradoras = resultAseguradora.Objects;

                return View(aseguradora);
            }
            else
            {
                //ERROR MENSAJE
            }
            return View();
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuarios = BL.Usuario.GetAllEF(usuario);
            aseguradora.Usuario = new ML.Usuario();
            if (resultUsuarios.Correct) 
            {
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                if (IdAseguradora == null) //Agregar
                {
                    return View(aseguradora);
                }
                else  //Actualizar
                {
                    ML.Result resultAseguradora = BL.Aseguradora.GetByIdEF(IdAseguradora.Value);
                    if(resultAseguradora.Correct)
                    {
                        aseguradora = ((ML.Aseguradora)resultAseguradora.Object);
                        aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                        return View(aseguradora);
                    }
                }
            }
            else
            {
                //Mensaje Error
            }
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            ML.Result resultAseguradora = new ML.Result();
            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                aseguradora.Imagen = ConvertToBytes(file);
            }
            if (aseguradora.IdAseguradora == 0) //Add
            {
                resultAseguradora = BL.Aseguradora.AddEF(aseguradora);
                if(resultAseguradora.Correct)
                {
                    ViewBag.Mensaje = "Aseguradora agregada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar la aseguradora" + resultAseguradora.ErrorMessage;
                }
            }
            else  //Actualizar
            {
                resultAseguradora = BL.Aseguradora.UpdateByIdEF(aseguradora);
                if(resultAseguradora.Correct)
                {
                    ViewBag.Mensaje = "Aseguradora actualizada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar la aseguradora" + resultAseguradora.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.DeleteEF(idAseguradora);
            if(result.Correct)
            {
                ViewBag.Mensaje = "Aseguradora eliminada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar la aseguradora";
            }
            return PartialView("Modal");
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] data = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            data = reader.ReadBytes((int)image.ContentLength);
            return data;
        }
    }
}