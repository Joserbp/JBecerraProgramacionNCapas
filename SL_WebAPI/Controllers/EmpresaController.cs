using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [Authorize]
    public class EmpresaController : ApiController
    {
        [Route("api/empresa/")]
        [HttpGet]
        public IHttpActionResult GetAll()   //POSTMAN OKp
        {
            ML.Result result = BL.Empresa.GetAllEF();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
        [Route("api/empresa/{IdEmpresa}")]
        [HttpGet]
        public IHttpActionResult GetById(int IdEmpresa) //POSTMAN OK
        {
            ML.Result result = BL.Empresa.GetByID(IdEmpresa);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
        [Route("api/empresa/")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Empresa empresa) //POSTMAN OK
        {
            ML.Result result = BL.Empresa.Add(empresa);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/empresa/")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]ML.Empresa empresa) //POSTMAN OK
        {
            ML.Result result = BL.Empresa.UpdateById(empresa);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/empresa/{IdEmpresa}")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.DeleteById(IdEmpresa);
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
