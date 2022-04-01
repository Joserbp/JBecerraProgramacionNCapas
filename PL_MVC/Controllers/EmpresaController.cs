using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PL_MVC.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.Empresas = new List<object>();

            /////ML.Result resultEmpresas = BL.Empresa.GetAllEF();
            //ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
            //var resultEmpresas = empresaSercivio.GetAll();

            //if (resultEmpresas.Correct)
            //{
            //    ML.Empresa empresa = new ML.Empresa();
            //    empresa.Empresas = resultEmpresas.Objects.ToList();

            //    return View(empresa);
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Ocurrio un error al mostrar la vista" + resultEmpresas.ErrorMessage;
            //    return PartialView("Modal");
            //}

            using (var client = new HttpClient())
            {
                var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(urlApi);

                if (Session["Token"] != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                }                

                var responseTask = client.GetAsync("empresa/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empresa objEmpresa = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empresa>(resultItem.ToString());
                        empresa.Empresas.Add(objEmpresa);
                    }
                    return View(empresa);
                }
                else
                {
                    if (result.StatusCode.ToString() == "Unauthorized")
                    {
                        ViewBag.Mensaje = "No tiene Permisos para ver esta información";
                        return PartialView("Modal");
                    }
                    ViewBag.Mensaje = "Ocurrio un error al mostrar la vista " + result.ReasonPhrase;
                    return PartialView("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {
            HttpPostedFileBase file = Request.Files["archivoTxt"];
            ML.Result resultError = ReadFile(file);
            if (resultError.Objects.Count > 0)
            {
                string fileError = Server.MapPath(@"~\Files\logErrores.txt");
                using (StreamWriter writer = new StreamWriter(fileError))
                {
                    foreach (string ln in resultError.Objects)
                    {
                        writer.WriteLine(ln);
                    }
                }
                Session["RutaDescarga"] = fileError;
                ViewBag.Mensaje = "Ocurrio un error al insertar las empresas, para mayor información descargue el Archivo Log.txt";
            }
            else
            {
                Session["RutaDescarga"] = null;
                ViewBag.Mensaje = "Todas las empresas fueron agregadas exitosamente";
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Result resultEmpresa = new ML.Result();
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null) //Add
            {
                return View(empresa);
            }
            else  //Update
            {
                //ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
                //var resultEmpresaService = empresaSercivio.GetById(IdEmpresa.Value);

                ////resultEmpresa = BL.Empresa.GetByID(IdEmpresa.Value);
                ////empresa = ((ML.Empresa)resultEmpresa.Object);
                //empresa = ((ML.Empresa)resultEmpresaService.Object);
                using (var client = new HttpClient())
                {
                    var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("empresa/" + IdEmpresa);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        empresa = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empresa>(readTask.Result.Object.ToString());

                        return View(empresa);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                        return PartialView("Modal");
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            ML.Result resultEmpresa = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                empresa.Logo = ConvertToBytes(file);
            }

            if (empresa.IdEmpresa == 0) //add
            {
                ////resultEmpresa = BL.Empresa.Add(empresa);
                //ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
                //var resultEmpresa1 = empresaSercivio.Add(empresa);

                //if (resultEmpresa1.Correct)
                //{
                //    ViewBag.Mensaje = "Empresa agregada correctamente";
                //}
                //else
                //{
                //    ViewBag.Mensaje = "Error al agregar la empresa" + resultEmpresa.ErrorMessage;
                //}
                using (var client = new HttpClient())
                {
                    var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.PostAsJsonAsync<ML.Empresa>("empresa/", empresa);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Empresa agregada correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al agregar la empresa";
                    }
                }
            }
            else //update
            {
                //ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
                //var resultEmpresa1 = empresaSercivio.Update(empresa);

                ////resultEmpresa = BL.Empresa.UpdateById(empresa);

                //if (resultEmpresa1.Correct)
                //{
                //    ViewBag.Mensaje = "Empresa actualizada correctamente";
                //}
                //else
                //{
                //    ViewBag.Mensaje = "Error al actualizar la empresa" + resultEmpresa1.ErrorMessage;
                //}
                using (var client = new HttpClient())
                {
                    var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.PutAsJsonAsync<ML.Empresa>("empresa/", empresa);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Empresa actualizada correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Erroral actualizar la empresa";
                    }
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            //ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
            //var resultEmpresa = empresaSercivio.Delete(IdEmpresa);
            ////ML.Result resultEmpresa = BL.Empresa.DeleteById(IdEmpresa);
            //if (resultEmpresa.Correct)
            //{
            //    ViewBag.Mensaje = "Empresa eliminada correctamente";
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Error al eliminar la empresa" + resultEmpresa.ErrorMessage;
            //}
            using (var client = new HttpClient())
            {
                var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.DeleteAsync("empresa/" + IdEmpresa);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Empresa eliminada correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al eliminar la empresa";
                }
            }
            return PartialView("Modal");
        }

        public ML.Result ReadFile(HttpPostedFileBase file)
        {
            StreamReader Textfile = new StreamReader(file.InputStream);
            ML.Result resultError = new ML.Result();
            resultError.Objects = new List<object>();
            string line;
            bool isFirstLine = true;
            while ((line = Textfile.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    line = Textfile.ReadLine();
                }
                String[] lines = line.Split('|');
                ML.Empresa empresa = new ML.Empresa();
                empresa.Nombre = lines[0];
                if (lines[1].ToString().Length > 10)
                {
                    empresa.Telefono = null;
                }
                else
                {
                    empresa.Telefono = lines[1];
                }
                empresa.Email = lines[2];
                empresa.DireccionWeb = lines[3];

                //ML.Result result = BL.Empresa.Add(empresa);
                ServiceEmpresa.EmpresaClient empresaSercivio = new ServiceEmpresa.EmpresaClient();
                var result = empresaSercivio.Add(empresa);
                if (!result.Correct)
                {
                    resultError.Objects.Add("Ocurrio un error al guardar el registro de la Empresa:" + empresa.Nombre + "  ---->  " + result.Ex.InnerException.Message);
                }
            }
            return resultError;
        }
        public ActionResult Download()
        {
            string file = Session["RutaDescarga"].ToString();
            string contentType = "text/plain";
            return File(file, contentType, Path.GetFileName(file));
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