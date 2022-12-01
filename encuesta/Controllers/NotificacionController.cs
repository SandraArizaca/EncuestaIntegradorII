using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using encuesta.Contexto;
using encuesta.DTO;
using encuesta.Models;
using encuesta.Utils;

namespace encuesta.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Codigo-Error, X-Mensaje-Error")]
    public class NotificacionController : ApiController
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        [HttpPost]
        public HttpResponseMessage Get(string d)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string respuesta = "";
            string IdUsuario = "";
            int NRol = 0;
            string CRuc = "";
            try
            {
                d = Util.desencriptarString(d);
                if (d != "")
                {
                    string[] aTrama = d.Split('^');
                    IdUsuario = aTrama[0];
                    NRol = int.Parse(aTrama[1]);
                    CRuc = aTrama[2];
                    List<NotificacionDto> xResult = DB.ListarEncuestasPendientes(IdUsuario, NRol, CRuc);
                    respuesta = new JavaScriptSerializer().Serialize(xResult);
                    //respuesta = Util.encriptarString(respuesta);
                    response = Request.CreateResponse<string>(HttpStatusCode.OK, respuesta);
                }
            }
            catch (Exception e)
            {
            }
            return response;
        }

    }
}