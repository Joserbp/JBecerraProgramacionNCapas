using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class PolizaController : Controller
    {
        // GET: Poliza
        public ActionResult GetAll()
        {
            ML.Result resultPolizas = BL.Poliza.GetAll();
            if (resultPolizas.Correct)
            {
                ML.Poliza poliza = new ML.Poliza();
                poliza.Polizas = resultPolizas.Objects;
                return View(poliza);
            }
            else
            {
                //
            }
            return View();
        }
        [HttpGet]
        public ActionResult Form(int? IdPoliza)
        {
            ML.Result resultSubPolizas = BL.SubPoliza.GetAll();
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuarios = BL.Usuario.GetAllEF(usuario);
            if (resultSubPolizas.Correct && resultUsuarios.Correct)
            {
                ML.Poliza poliza = new ML.Poliza();
                poliza.SubPoliza = new ML.SubPoliza();
                poliza.SubPoliza.SubPolizas = resultSubPolizas.Objects;
                poliza.Usuario = new ML.Usuario();
                poliza.Usuario.Usuarios = resultUsuarios.Objects;
                if (IdPoliza == null) //Add
                {
                    return View(poliza);
                }
                else //Update
                {
                    ML.Result resultPoliza = BL.Poliza.GetById(IdPoliza.Value);
                    if (resultPoliza.Correct)
                    {
                        poliza = ((ML.Poliza)resultPoliza.Object);
                        poliza.SubPoliza.SubPolizas = resultSubPolizas.Objects;
                        poliza.Usuario.Usuarios = resultUsuarios.Objects;
                        return View(poliza);
                    }
                    else
                    {
                        //error
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            if (poliza.IdPoliza == 0) //add
            {
                result = BL.Poliza.Add(poliza);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Poliza Agregada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "La poliza no se agrego exitosamente" + result.ErrorMessage;
                }
            }
            else //Update
            {
                result = BL.Poliza.Update(poliza);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Poliza actualizada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "La poliza no se actualizo exitosamente" + result.ErrorMessage;
                }
            }


            return PartialView("Modal");
        }
        public ActionResult Delete(int IdPoliza)
        {
            ML.Result result = BL.Poliza.Delete(IdPoliza);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Poliza Eliminada Exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar la Poliza" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}