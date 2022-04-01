using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace PL_MVC.Controllers
{
    public class CargaMasivaUsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }


        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["FileExcel"];
            string pathFolder = ConfigurationManager.AppSettings["pathFolder"].ToString();

            if (Session["PathArchivo"] == null)
            {
                if (file.ContentLength > 0)
                {
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionModulo = ConfigurationManager.AppSettings["TipoExcel"].ToString();

                    if (extensionArchivo == extensionModulo)
                    {
                        string pathArchivo = Server.MapPath(pathFolder) + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(pathArchivo))
                        {
                            file.SaveAs(pathArchivo);
                            string connectionString = ConfigurationManager.AppSettings["ConnectionStringExcel"].ToString() + pathArchivo; ;
                            ML.Result resultUsuarios = BL.Usuario.GetAllByExcel(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.Validacion(resultUsuarios.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    Session["PathArchivo"] = pathArchivo;
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Mensaje = "El excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
            }
            else
            {
                string rutaArchivoExcel = Session["PathArchivo"].ToString();
                string connectionString = ConfigurationManager.AppSettings["ConnectionStringExcel"].ToString() + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.GetAllByExcel(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        usuarioItem.Direccion = new ML.Direccion();
                        usuarioItem.Direccion.IdDireccion = 1;
                        usuarioItem.Rol = new ML.Rol();
                        usuarioItem.Rol.IdRol = 1;
                        
                        ML.Result resultAdd = BL.Usuario.AddEF(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el usuario con nombre: " + usuarioItem.Nombre + " Apellido Paterno:" + usuarioItem.ApellidoPaterno +
                            " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string fileError = Server.MapPath(@"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Mensaje = "Los usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Los usuarios han sido registrados correctamente";
                    }

                }

            }
            return PartialView("Modal");
        }
    }
}