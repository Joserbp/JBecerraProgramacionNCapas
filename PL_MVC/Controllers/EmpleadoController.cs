using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PL_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();
            if (result.Correct)
            {
                ML.Empleado empleado = new ML.Empleado();
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                //error
            }
            return View();
        }
        [HttpGet]
        public ActionResult Form(string numeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpleado = new ML.Result();
            ML.Result resultEmpresa = BL.Empresa.GetAllEF();
            if (resultEmpresa.Correct)
            {
                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = new List<object>();
                if (numeroEmpleado == null) //add
                {
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    empleado.Action = "Add";
                    return View(empleado);
                }
                else //Update
                {
                    resultEmpleado = BL.Empleado.GetByNumeroEmpleado(numeroEmpleado);
                    empleado = ((ML.Empleado)resultEmpleado.Object);
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    empleado.Action = "Update";
                    return View(empleado);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                empleado.Foto = ConvertToBytes(file);
            }
            if (empleado.Action == "Add")
            {
                result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Empreado agregado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Empreado no ha sido agregado exitosamente";
                }
            }
            else if (empleado.Action == "Update")
            {
                result = BL.Empleado.Update(empleado);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Empreado actualizado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Empreado no ha sido actualizado exitosamente";
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error el Action no es reconocido";
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(string numeroEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(numeroEmpleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empleado eliminado exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar al Empleado";
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