using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresas = BL.Empresa.GetAllEF();
            ML.Result resultEmpleados = BL.Empleado.GetAll();
            if (resultEmpresas.Correct)
            {
                empleado.Empresa = new ML.Empresa();
                if (resultEmpleados.Correct)
                {
                    empleado.Empleados = resultEmpleados.Objects;
                    empleado.Empresa.Empresas = resultEmpresas.Objects;
                    return View(empleado);
                }
                else
                {
                    //error
                }
            }
            else
            {
                //error
            }

            return View();
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresas = BL.Empresa.GetAllEF();
            ML.Result resultEmpleados = new ML.Result();
            if (empresa.IdEmpresa == 0)
            {
                resultEmpleados = BL.Empleado.GetAll();
            }
            else
            {
                resultEmpleados = BL.Empleado.GetByIdEmpresa(empresa.IdEmpresa);
            }
            if (resultEmpresas.Correct)
            {
                empleado.Empresa = new ML.Empresa();
                if (resultEmpleados.Correct)
                {
                    empleado.Empleados = resultEmpleados.Objects;
                    empleado.Empresa.Empresas = resultEmpresas.Objects;
                    return View(empleado);
                }
                else
                {
                    //error
                }
            }
            else
            {
                //error
            }

            return View();
        }
        [HttpGet]
        public ActionResult Dependiente(string numeroEmpleado)
       {
            if (numeroEmpleado == null)
            {
                numeroEmpleado = Session["NumeroEmpleado"].ToString();
            }
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultDependientes = BL.Dependiente.GetByNumeroEmpleado(numeroEmpleado);
            if (resultDependientes.Correct)
            {
                Session["NumeroEmpleado"] = numeroEmpleado;
                dependiente.Dependientes = new List<object>();
                dependiente.Dependientes = resultDependientes.Objects;
                return View(dependiente);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al cargar los dependientes" + resultDependientes.ErrorMessage;
                return PartialView("Modal");
            }
        }
        [HttpGet]
        public ActionResult Form(int? IdDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultDependientesTipo = BL.DependienteTipo.GetAllEF();
            if (resultDependientesTipo.Correct)
            {
                if (IdDependiente == null) //add
                {
                    dependiente.Empleado = new ML.Empleado();
                    dependiente.Empleado.NumeroEmpleado = Session["NumeroEmpleado"].ToString();
                    dependiente.DependienteTipo = new ML.DependienteTipo();
                    dependiente.DependienteTipo.DependientesTipo = resultDependientesTipo.Objects;
                    return View(dependiente);
                }
                else  //Update
                {
                    ML.Result resultDependiente = BL.Dependiente.GetById(IdDependiente.Value);
                    if (resultDependiente.Correct)
                    {
                        dependiente = ((ML.Dependiente)resultDependiente.Object);
                        dependiente.DependienteTipo.DependientesTipo = resultDependientesTipo.Objects;
                        return View(dependiente);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al obtener el dependiente" + resultDependiente.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al obtener algunos datos" + resultDependientesTipo.ErrorMessage;
                return PartialView("Modal");
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            if (dependiente.IdDependiente == 0) //add
            {
                result = BL.Dependiente.Add(dependiente);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El Dependiente se agrego Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Dependiente no se agrego Correctamente" + result.ErrorMessage;
                }
            }
            else //update
            {
                result = BL.Dependiente.Update(dependiente);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El Dependiente se actualizo Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Dependiente no se actualizo Correctamente" + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int IdDependiente)
        {
            ML.Result result = BL.Dependiente.Delete(IdDependiente);
            if (result.Correct)
            {
                ViewBag.Mensaje = "El dependiente se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}