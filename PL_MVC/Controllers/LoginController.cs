using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                string uriApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(uriApi);

                var responseTask = client.PostAsJsonAsync("login/authenticate", usuario);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    Session["Token"] = readTask.Result;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Usuario y/o Contraseña Incorrectos";
                    return PartialView("Modal");
                }
            }
            //ML.Result result = BL.Usuario.GetByUserNameEF(usuario.UserName);
            //if (result.Correct)
            //{
            //    if (((ML.Usuario)result.Object).Password == usuario.Password)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "Contraseña Incorrecta";
            //        return PartialView("Modal");
            //    }
            //}
            //else
            //{
            //    ViewBag.Mensaje = "El Usuario y/o Contraseña son Incorrectos";
            //    return PartialView("Modal");
            //}
        }
    }
}