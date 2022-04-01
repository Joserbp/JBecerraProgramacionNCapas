using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;

namespace SL_WebAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }


        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok("IPrincipal-user: " + identity.Name +"- IsAuthenticated: " + identity.IsAuthenticated);
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(ML.Usuario usuario)
        {
            if (usuario == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                ML.Result result = BL.Usuario.GetByUserNameEF(usuario.UserName);
                if (result.Correct)
                {
                    if (((ML.Usuario)result.Object).Password == usuario.Password)
                    {
                        var token = TokenGenerator.GenerateTokenJwt(usuario.UserName);
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized(); //corregir
                }
            }
         
        }
    }
}
