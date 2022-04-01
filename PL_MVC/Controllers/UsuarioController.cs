using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Http;
using System.Configuration;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();
            ////ML.Result resultUsuario = BL.Usuario.GetAllEF(usuario);

            //ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
            //var resultUsuario = usuarioServicio.GetAll(usuario);

            //if (resultUsuario.Correct)
            //{
            //    usuario.Usuarios = resultUsuario.Objects.ToList();
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Error al mostrar la vista" + resultUsuario.ErrorMessage;
            //    return PartialView("Modal");
            //}
            using (var client = new HttpClient())
            {  //MVC OK

                string urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("usuario/");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al obtener la informacion";
                    return PartialView("Modal");
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAllEF(usuario);

            ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
            var result = usuarioServicio.GetAll(usuario);


            if (result.Correct)
            {
                usuario.Usuarios = result.Objects.ToList();
                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Error al mostrar la vista" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAllEF();
            ML.Result resultPais = BL.Pais.GetAllEF();

            if (resultRol.Correct && resultPais.Correct)
            {
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                if (IdUsuario == null) //ADD
                {
                    return View(usuario);
                }
                else //UPDATE                      
                {
                    ////ML.Result resultUsuario = BL.Usuario.GetByIdEF(IdUsuario.Value);
                    //ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
                    //var resultUsuario = usuarioServicio.GetById(IdUsuario.Value);

                    //if (resultUsuario.Correct)
                    //{
                    //    usuario = ((ML.Usuario)resultUsuario.Object);
                    //    ML.Result resultColonia = BL.Colonia.GetAllByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    //    ML.Result resultMunicipio = BL.Municipio.GetAllByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    //    ML.Result resultEstado = BL.Estado.GetAllByPaisEF(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    //    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    //    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    //    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    //    usuario.Rol.Roles = resultRol.Objects;


                    //    return View(usuario);
                    //}
                    using (var client = new HttpClient())
                    {

                        string urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("usuario/" + IdUsuario);
                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            ML.Result resultColonia = BL.Colonia.GetAllByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                            ML.Result resultMunicipio = BL.Municipio.GetAllByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                            ML.Result resultEstado = BL.Estado.GetAllByPaisEF(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                            usuario.Rol.Roles = resultRol.Objects;
                            return View(usuario);
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un error al obtener la informacion";
                            return PartialView("Modal");
                        }
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Error al mostrar la vista" + resultRol.ErrorMessage;
                return PartialView("Modal");
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                //ML.Result result = new ML.Result();
                ML.Result resultDireccion = new ML.Result();

                HttpPostedFileBase file = Request.Files["ImagenData"];

                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertToBytes(file);
                }

                if (usuario.IdUsuario == 0) //Add Usuario
                {
                    resultDireccion = BL.Direccion.AddEF(usuario.Direccion);
                    if (resultDireccion.Correct)
                    {
                        usuario.Direccion.IdDireccion = ((int)resultDireccion.Object);
                        ////result = BL.Usuario.AddEF(usuario);

                        //ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
                        //var result = usuarioServicio.Add(usuario);
                        //if (result.Correct)
                        //{
                        //    ViewBag.Mensaje = "El usuario ha sido registrado exitosamente";
                        //}
                        //else
                        //{
                        //    ViewBag.Mensaje = "El usuario no ha registrado exitosamente" + result.ErrorMessage;
                        //}
                        using (var client = new HttpClient())
                        {
                            string urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                            client.BaseAddress = new Uri(urlApi);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("usuario/", usuario);
                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                ViewBag.Mensaje = "El usuario ha sido registrado exitosamente";
                            }
                            else
                            {
                                ViewBag.Mensaje = "El usuario no ha registrado exitosamente";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "La Direccion no se agrego exitosamente" + resultDireccion.ErrorMessage;
                    }
                }
                else  //Udpdate Usuario
                {
                    resultDireccion = BL.Direccion.UpdateById(usuario.Direccion);
                    ////result = BL.Usuario.UpdateByIdEF(usuario);
                    //ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
                    //var result = usuarioServicio.Update(usuario);

                    //if (result.Correct && resultDireccion.Correct)
                    //{
                    //    ViewBag.Mensaje = "El usuario ha sido actualizado exitosamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "El usuario no se ha actualizado exitosamente" + result.ErrorMessage;
                    //}
                    using (var client = new HttpClient())
                    {
                        string urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.PutAsJsonAsync<ML.Usuario>("usuario/", usuario);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "El usuario ha sido actualizado exitosamente";
                        }
                        else
                        {
                            ViewBag.Mensaje = "El usuario no se ha actualizado exitosamente";
                        }
                    }
                }
            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAllEF();
                ML.Result resultPais = BL.Pais.GetAllEF();
                ML.Result resultColonia = BL.Colonia.GetAllByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                ML.Result resultMunicipio = BL.Municipio.GetAllByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                ML.Result resultEstado = BL.Estado.GetAllByPaisEF(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                usuario.Rol.Roles = resultRol.Objects;
                return View(usuario);
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario, int IdDireccion)
        {
            ////ML.Result result = BL.Usuario.DeleteByIdEF(IdUsuario);

            //ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
            //var result = usuarioServicio.Delete(IdUsuario);

            //if (result.Correct)
            //{
            //    ML.Result resultDireccion = BL.Direccion.DeleteById(IdDireccion);
            //    if (resultDireccion.Correct)
            //    {
            //        ViewBag.Mensaje = "El usuario y su direccion han sido eliminados exitosamente";
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "El usuario fue eliminado exitosamente, Error al eliminar la direccion" + resultDireccion.ErrorMessage;
            //    }
            //}
            //else
            //{
            //    ViewBag.Mensaje = "El usuario y su direccion no se han eliminado exitosamente" + result.ErrorMessage;
            //}
            using (var client = new HttpClient())
            {
                var urlApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.DeleteAsync("usuario/" + IdUsuario);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ML.Result resultDireccion = BL.Direccion.DeleteById(IdDireccion);
                    if (resultDireccion.Correct)
                    {
                        ViewBag.Mensaje = "El usuario y su direccion han sido eliminados exitosamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El usuario fue eliminado exitosamente, Error al eliminar la direccion" + resultDireccion.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "El usuario y su direccion no se han eliminado exitosamente";
                }

            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult UpdateStatus(int IdUsuario)
        {
            //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            ServiceUsuario.UsuarioClient usuarioServicio = new ServiceUsuario.UsuarioClient();
            var result = usuarioServicio.GetById(IdUsuario);

            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = ((ML.Usuario)result.Object);

                usuario.Estatus = (usuario.Estatus) ? false : true;

                //result = BL.Usuario.UpdateByIdEF(usuario);
                result = usuarioServicio.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El estatus del usuario se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un erro al actualizar el Estatus de usuario" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un erro al actualizar el Estatus de usuario" + result.ErrorMessage;
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

        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetAllByPaisEF(IdPais);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetAllByIdEstadoEF(IdEstado);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetAllByIdMunicipioEF(IdMunicipio);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
    }
}