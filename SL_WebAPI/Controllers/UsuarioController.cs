using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        [Route("api/usuario/")]
        [HttpGet]
        public IHttpActionResult GetAll()  //POSTMAN OK
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }

        }
        [Route("api/usuario/{IdUsuario}")]
        [HttpGet]
        public IHttpActionResult GetById(int IdUsuario)  //POSTMAN OK
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }

        }
        [Route("api/usuario/")]
        [HttpPost]
        public IHttpActionResult Add(ML.Usuario usuario) //POSTMAN OK
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.Created, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }

        }
        [Route("api/usuario/")]
        [HttpPut]
        public IHttpActionResult Update(ML.Usuario usuario) //POSTMAN OK
        {
            ML.Result result = BL.Usuario.UpdateByIdEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/usuario/{IdUsuario}")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdUsuario)  //POSTMAN OK
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.DeleteByIdEF(IdUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
