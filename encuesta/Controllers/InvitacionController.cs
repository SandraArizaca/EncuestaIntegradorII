using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using encuesta.Contexto;
using encuesta.Models;
using OfficeOpenXml;
using System.Net.Http;
using System.Configuration;
using System.Net.Mail;
using encuesta.FIlters;
using System.Threading.Tasks;
using System.Threading;

namespace encuesta.Controllers
{
    [TokenFilter]
    public class InvitacionController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        public ActionResult AgregarContacto(int? ID_Cuestionario, int? ID_Publico)
        {
            int idUsuario = 0;
            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            if (!ID_Publico.HasValue) { ID_Publico = 0; }
            ViewBag.ID_Cuestionario = ID_Cuestionario;
            ViewBag.ID_Publico = ID_Publico;
            ViewBag.ID_Usuario = idUsuario;
            return PartialView();
        }

        public ActionResult ImportarFile(int? ID_Cuestionario, int? ID_Publico)
        {
            int idUsuario = 0;

            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            if (!ID_Publico.HasValue) { ID_Publico = 0; }
            ViewBag.ID_Cuestionario = ID_Cuestionario;
            ViewBag.ID_Usuario = idUsuario;
            ViewBag.ID_Publico = ID_Publico;
            TempData["IDPublico"] = ID_Publico;
            TempData.Keep("IDPublico");
            TempData["ID_Cuestionario"] = ID_Cuestionario;
            TempData.Keep("ID_Cuestionario");

            return PartialView();
        }

        [HttpPost]
        public JsonResult UploadData()
        {
            int idUsuario = 0;

            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            int idPublico = 0;

            if (TempData.ContainsKey("IDPublico"))
            {
                idPublico = Convert.ToInt32(TempData.Peek("IDPublico"));
            }

            int idCuestionario = 0;

            if (TempData.ContainsKey("ID_Cuestionario"))
            {
                idCuestionario = Convert.ToInt32(TempData.Peek("ID_Cuestionario"));
            }

            DB.EliminaCargaDatos(idUsuario);

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files[0];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var DirectorioEntidadesList = new List<TMP_DirectorioEntidades>();
                    var DirectorioProveedorList = new List<TMP_DirectorioProveedores>();
                    
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        
                        for(int i = 1; i <= currentSheet.Count; i++)
                        {
                            if(idPublico == 2)
                            {
                                if (i == 1)
                                { 
                                    var workSheet = currentSheet[i];
                                    var noOfCol = workSheet.Dimension.End.Column;
                                    var noOfRow = workSheet.Dimension.End.Row;
                                    string contenido = "";
                                    using (var db = new CTX_Encuesta())
                                    {
                                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                        {
                                            var entidades = new TMP_DirectorioEntidades();
                                            entidades.C_RucEntidad = (workSheet.Cells[rowIterator, 1].Value ?? "").ToString();
                                            entidades.C_NombreEntidad = (workSheet.Cells[rowIterator, 2].Value ?? "").ToString();
                                            entidades.C_TipoEntidad = (workSheet.Cells[rowIterator, 3].Value ?? "").ToString();
                                            entidades.C_DepartamentoEntidad = (workSheet.Cells[rowIterator, 4].Value ?? "").ToString();
                                            entidades.C_IDUsuario = (workSheet.Cells[rowIterator, 5].Value ?? "").ToString();
                                            entidades.C_NombreLogistico = (workSheet.Cells[rowIterator, 6].Value ?? "").ToString();
                                            entidades.C_CorreoLogistico = (workSheet.Cells[rowIterator, 7].Value ?? "").ToString();
                                            entidades.ID_UsuarioCarga = idUsuario;

                                            contenido += entidades.C_RucEntidad + "^" + entidades.C_NombreEntidad + "^" + entidades.C_TipoEntidad + "^" + entidades.C_DepartamentoEntidad + "^" + entidades.C_IDUsuario + "^" + entidades.C_NombreLogistico + "^" + entidades.C_CorreoLogistico + "^" + entidades.ID_UsuarioCarga + "¬";
                                        }
                                        var objDAO = new AccesoDatos();
                                        string res=objDAO.ejecutarComandoWithParameters("PA_TMP_DirectorioEntidades_Ins", contenido,1000);
                                        if (res == "")
                                        {
                                            TempData["Msg"] = "Ocurrió un error al procesar el archivo!";
                                        }
                                    }
                                }
                            }

                            if (idPublico == 3)
                            {
                                if (i == 2)
                                { 
                                    var workSheet = currentSheet[i];
                                    var noOfCol = workSheet.Dimension.End.Column;
                                    var noOfRow = workSheet.Dimension.End.Row;
                                    string contenido = "";

                                    using (var db2 = new CTX_Encuesta())
                                    {
                                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                         {
                                            var proveedores = new TMP_DirectorioProveedores();
                                            proveedores.C_RucProveedor = (workSheet.Cells[rowIterator, 1].Value ?? "").ToString();
                                            proveedores.C_Proveedor = (workSheet.Cells[rowIterator, 2].Value ?? "").ToString();
                                            proveedores.C_TipoEmpresa = (workSheet.Cells[rowIterator, 3].Value ?? "").ToString();
                                            proveedores.C_DepartamentoProveedor = (workSheet.Cells[rowIterator, 4].Value ?? "").ToString();
                                            proveedores.C_IDUsuario = (workSheet.Cells[rowIterator, 5].Value ?? "").ToString();
                                            proveedores.C_Gestor = (workSheet.Cells[rowIterator, 6].Value ?? "").ToString();
                                            proveedores.C_CorreoProveedor = (workSheet.Cells[rowIterator, 7].Value ?? "").ToString();
                                            proveedores.C_CorreoUsuario = (workSheet.Cells[rowIterator, 8].Value ?? "").ToString();
                                            proveedores.ID_UsuarioCarga = idUsuario;

                                            contenido += proveedores.C_RucProveedor + "^" + proveedores.C_Proveedor + "^" + proveedores.C_TipoEmpresa + "^" + proveedores.C_DepartamentoProveedor + "^" + proveedores.C_IDUsuario + "^" + proveedores.C_Gestor + "^" + proveedores.C_CorreoProveedor + "^" + proveedores.C_CorreoUsuario  + "^" + proveedores.ID_UsuarioCarga + "¬";
                                        }
                                        
                                        var objDAO = new AccesoDatos();
                                        string res = objDAO.ejecutarComandoWithParameters("PA_TMP_DirectorioProveedores_Ins", contenido, 1000);
                                        if (res == "")
                                        {
                                            TempData["Msg"] = "Ocurrió un error al procesar el archivo!";
                                        }
                                    }
                                }
                            }

                            if (idPublico != 2 && idPublico != 3)
                            {
                                if (i == 1)
                                {
                                    var workSheet = currentSheet[i];
                                    var noOfCol = workSheet.Dimension.End.Column;
                                    var noOfRow = workSheet.Dimension.End.Row;
                                    string contenido = "";
                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {
                                        String C_RucEntidad = (workSheet.Cells[rowIterator, 1].Value ?? "").ToString();
                                        String C_NombreEntidad = (workSheet.Cells[rowIterator, 2].Value ?? "").ToString();
                                        String C_TipoEntidad = (workSheet.Cells[rowIterator, 3].Value ?? "").ToString();
                                        String C_DepartamentoEntidad = (workSheet.Cells[rowIterator, 4].Value ?? "").ToString();
                                        String C_IDUsuario = (workSheet.Cells[rowIterator, 5].Value ?? "").ToString();
                                        String C_NombreLogistico = (workSheet.Cells[rowIterator, 6].Value ?? "").ToString();
                                        String C_CorreoLogistico = (workSheet.Cells[rowIterator, 7].Value ?? "").ToString();

                                        contenido += C_RucEntidad + "^" +
                                            C_NombreEntidad + "^" +
                                            C_TipoEntidad + "^" +
                                            C_DepartamentoEntidad + "^" +
                                            C_IDUsuario + "^" +
                                            C_NombreLogistico + "^" +
                                            C_CorreoLogistico + "^" +
                                            idUsuario + "^" +
                                            idPublico + "^" +
                                            idCuestionario + "¬";
                                    }
                                    var objDAO = new AccesoDatos();
                                    string res = objDAO.ejecutarComandoWithParameters("PA_TMP_Directorio_Ins", contenido, 1000);
                                    if (res == "")
                                    {
                                        TempData["Msg"] = "Ocurrió un error al procesar el archivo!";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Json(idUsuario, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Carga_datos(int? pUsuario, int? pPublico)
        {
            if (!pUsuario.HasValue) { pUsuario = 0; }
            if (!pPublico.HasValue) { pPublico = 0; }
            List<TMP_CargaDatos> lst = new List<TMP_CargaDatos>();
            lst = DB.ListCargaDatos(pUsuario.Value);
            ViewBag.ID_Publico = pPublico.Value;
            return PartialView(lst);
        }

        [HttpPost]
        public JsonResult InvitacionUsuarios(int? pCuestionario)
        {
            int idUsuario = 0;

            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            DB.InsertarInvitacionUsuarios(pCuestionario.Value, idUsuario);
            return Json(pCuestionario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegistrarUsuarioInvitacion(int? ID_Cuestionario, int? ID_Publico, int? ID_Usuario, string C_Ruc, string C_RazonSocial, string C_Tipo, string C_Ubigeo, string C_TipoDocumento, string C_NroDocumento, string C_NombreCompleto, string C_Correo)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            if (!ID_Publico.HasValue) { ID_Publico = 0; }
            if (!ID_Usuario.HasValue) { ID_Usuario = 0; }

            DB.RegistrarUsuarioInvitacion(ID_Cuestionario.Value, ID_Publico.Value, ID_Usuario.Value, C_Ruc, C_RazonSocial, C_Tipo, C_Ubigeo, C_TipoDocumento, C_NroDocumento, C_NombreCompleto, C_Correo);

            return RedirectToAction("Directorio", "Encuesta", new { N_Cuestionario = ID_Cuestionario });
        }

        [HttpGet]
        public async Task<JsonResult> EnviarInvitacion(int ID_Usuario, int ID_Cuestionario, int ID_Correo)
        {
            string dnsSetting = ConfigurationManager.AppSettings["DNS_Server"].ToString();
            string registrosEnviar = ConfigurationManager.AppSettings["registros_enviar"].ToString();
            string sRpta = "OK";

            List<T_AccesoInvitacion> lstInvitacion = DB.ListarAccesoInvitacionesNoEnviadas(ID_Usuario, ID_Cuestionario, ID_Correo, 0);

            if (lstInvitacion.Count <= Int32.Parse(registrosEnviar))
            {
                sRpta = EnviarInvitacionR(ID_Usuario, ID_Cuestionario, ID_Correo, Int32.Parse(registrosEnviar));
            }
            else
            {
                int bucle = (lstInvitacion.Count / Int32.Parse(registrosEnviar)) + 1;

                for (int i = 0; i <= bucle; i++)
                {
                    sRpta = EnviarInvitacionR(ID_Usuario, ID_Cuestionario, ID_Correo, Int32.Parse(registrosEnviar));
                    Thread.Sleep(3000);
                }
            }
            return Json(sRpta, JsonRequestBehavior.AllowGet);
        }

        private string EnviarInvitacionR(int ID_Usuario, int ID_Cuestionario, int ID_Correo, int reg)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }

            string dnsSetting = ConfigurationManager.AppSettings["DNS_Server"].ToString();
            string sRpta = "OK";

            List<T_AccesoInvitacion> lstInvitacion = DB.ListarAccesoInvitacionesNoEnviadas(ID_Usuario, ID_Cuestionario, ID_Correo, reg);
            T_Cuestionario Cuestionario = DB.T_Cuestionario.Find(ID_Cuestionario);


            if (lstInvitacion.Count > 0)
            {
                foreach (var oInvita in lstInvitacion)
                {
                    //Construye Correo
                    string sToken = oInvita.C_Token;
                    string sRazonSocial = oInvita.C_RazonSocial;
                    string sNombre = oInvita.C_NombreCompleto;
                    string EMail = oInvita.C_Correo;
                    int iTipoA = oInvita.ID_Area;
                    int iTipoP = oInvita.ID_Publico;
                    string strLink = "https://" + dnsSetting + "/Accesos/token?token=" + sToken;

                    string strEmailPart = EMail.Substring(0, 5) + "**********";

                    string body = generaMensaje(sRazonSocial, sNombre, strLink, iTipoA, iTipoP, 0, Cuestionario);
                    sRpta = EnviarCorreo(EMail, body, Usuario, oInvita.ID_Invitacion, 6);
                }
            }
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            return sRpta;
        }

        private string generaMensaje(string razon_social, string nombre_completo, string enlace, int tipo_area, int tipo_publico, int reenviar, T_Cuestionario cuestionario)
        {
            string response = @"
                    <html>
                        <head>
                        <meta http-equiv=""Content - Type"" content=""text/html; charset=us-ascii"">
                        " + estilos() + @"
                        <body bgcolor=""#f4f6f7"">
                            <table style=""width:800px !important"" bgcolor=""#f4f6f7"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table bgcolor=""#ffffff"" class=""content"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                <tbody>
                                                    " + cabecera(tipo_area, reenviar) + @"
                                                    " + cuerpo(razon_social, nombre_completo, enlace, tipo_area, tipo_publico, reenviar, cuestionario) + @"
                                                    " + pie(tipo_area) + @"
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </body>
                    </html>
                ";

            return response;
        }

        private string EnviarCorreo(string e_mail, string body, string Usuario, int ID_Invitacion, int ID_EstadoI)
        {
            int proc = 0;
            if(ID_EstadoI == 6) //Enviar Invitacion
            {
                proc = 1;
            }

            if (ID_EstadoI == 9) //Reenviar Invitacion
            {
                proc = 2;
            }

            var message = new MailMessage();


            List<T_Cuestionario> lstCuestionario = DB.ListCuestionario(ID_Invitacion);
           
            message.From = new MailAddress(lstCuestionario.ElementAt(0).C_CorreoRemitente, "PERU COMPRAS");
            
            message.To.Add(new MailAddress(e_mail));
            message.Subject = "PERU COMPRAS - Responde la Encuesta";
            message.Body = body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    DB.ActualizarEstadoInvitacion(ID_Invitacion, Usuario, ID_EstadoI, proc);
                    return "OK";
                }
                catch (Exception e)
                {
                    DB.ActualizarEstadoInvitacion(ID_Invitacion, Usuario, 8, proc);
                    return e.Message + " - " + e.InnerException;
                }
            }
            return "OK";
        }

        private string estilos()
        {
            return @"
                <style type=""text/css"">
                    body {margin: 0; padding: 0; min-width: 100%!important;}
                    img {height: auto;}
                    .content {width: 100%; max-width: 600px;}
                    .header {padding: 40px 30px 20px 30px;}
                    .innerpadding {padding: 30px 30px 30px 30px;}
                    .borderbottom {border-bottom: 1px solid #f2eeed;}
                    .subhead {font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px;}
                    .h1, .h2, .bodycopy {color: #e8eff2; font-family: sans-serif;}
                    .h2, .h3, .h4, .bodycopy {color: #153643; font-family: sans-serif;}
                    .h1 {font-size: 33px; line-height: 38px; font-weight: bold;}
                    .h2 {padding: 0 0 15px 0; font-size: 15px; line-height: 15px; font-weight: bold;}
                    .h3 {padding: 0 0 10px 0; font-size: 15px; line-height: 20px; font-weight: bold;}
                    .h4 {padding: 0 0 15px 0; font-size: 15px; line-height: 15px;}
                    .bodycopy {font-size: 14px; line-height: 22px; text-align:justify;}
                    .bodycopy2 {font-size: 16px; line-height: 22px; text-align:center;}
                    .firma {font-size: 12px; line-height: 16px;}
                    .footer {padding: 20px 30px 15px 30px;}
                    .footercopy {font-family: sans-serif; font-size: 14px; color: #ffffff;}
                    .enlace {color:#fff;padding:10px;background-color:#d1efff;font-size:16px;font-weight:bold;}
                    .enlaceC {color:#fff;padding:10px;font-size:16px;font-weight:bold;}
                    .enlace>a:-webkit-any-link {color:#fff !important;text-decoration:none !important;}
                    .button {-webkit-appearance: button; -moz-appearance: button; appearance: button; text-decoration: none; color:#fe0000}
                </style>
                ";
        }

        private string cabecera(int tipo_area, int reenviar)
        {
            string ImagenesEncuesta = ConfigurationManager.AppSettings["ImagenesEncuesta"].ToString();
            ImagenesEncuesta = ImagenesEncuesta + "rotulo_EncuestaGeneral.jpg";

            return @"
                    <tr>
                        <td>
                            <table style=""width:800px !important"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                <tbody>
                                    <tr>
                                        <td>
                                            <img style=""width:860px"" width=""860"" class=""fix"" src=""" + ImagenesEncuesta + @""">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                ";
        }

        private string cuerpo(string razon_social, string nombre_completo, string enlace, int tipo_area, int tipo_publico, int reenviar, T_Cuestionario cuestionario)
        {
            string response = "";
            string tituloDestinatario = @"<tr><td class=""h2"">" + nombre_completo + @"</td></tr>
                                        <tr><td class=""h4"">" + razon_social + @"</td></tr>";
            if (nombre_completo.Trim().ToUpper() == razon_social.Trim().ToUpper())
            {
                tituloDestinatario = @"<tr><td class=""h4"">" + razon_social + @"</td></tr>";
            }

            if(tipo_area == 1) // Acuerdo Marco
            {
                if(reenviar == 0)
                {
                    int tamano = (cuestionario.C_Cuestionario.Length >= 24)? 24 : cuestionario.C_Cuestionario.Length;
                    
                    string nombreCuestionario = cuestionario.C_Cuestionario.Substring(0, tamano);
                    if (nombreCuestionario == "ENCUESTA DE SATISFACCIÓN")
                    {
                        response = @"
	                    <tr>
		                    <td class=""innerpadding borderbottom"">
			                    <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
				                    <tbody>
					                    <tr><td class=""h3"">Estimado(a) usuario(a):</td></tr>
					                    " + tituloDestinatario + @"
					                    <tr>
						                    <td class=""bodycopy"">
							                    <p>
							                    Porque tu opinión es muy importante para nosotros. La Central de Compras Públicas – PERÚ COMPRAS, 
							                    te ha seleccionado para responder la Encuesta de Satisfacción del Método Especial de Contratación 
							                    de Acuerdo Marco, porque nos permitirá medir el grado de satisfacción de los Catálogos Electrónicos 
							                    de Acuerdos Marco respecto a la operatividad de los Catálogos Electrónicos para el periodo de enero 
							                    a diciembre del año 2021 e identificar oportunidad de mejoras de este servicio.
							                    </p>
							                    <p>Para responder la encuesta, haga clic en el siguiente enlace: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span> 
							                    o copie y pegue la siguiente dirección URL en su navegador de internet:</p>
							                    <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
							                    <p>Muchas gracias por su tiempo.</p>
							                    <br />
						                    </td>
					                    </tr>
					                    <tr>
						                    <td class=""firma"">
							                    <hr />
							                    Dirección de Análisis de Mercado - DAMER<br />
							                    Central de Compras Públicas – PERÚ COMPRAS<br />
							                    Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
							                    Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a>
						                    </td>
					                    </tr>
				                    </tbody>
			                    </table>
		                    </td>
	                    </tr>
	                    ";
                    }
                    else
                    {
                        response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado/a Usuario/a:</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Previo cordial saludo, le comunicamos que, en atención al proceso de homologación que la 
                                                Presidencia del Consejo de Ministros – PCM viene desarrollando para lograr la aprobación de 12 
                                                proyectos de Fichas de Homologación (FH) de equipos de cómputo; PERÚ COMPRAS requiere 
                                                estimar el impacto que tendría esta aprobación en las operaciones de los Catálogos Electrónicos 
                                                de computadoras.
                                                </p><p>
                                                En ese sentido, lo invitamos a responder la encuesta sobre el <b>“Impacto de la aprobación de 
                                                Fichas de Homologación en los Catálogos Electrónicos de computadoras”</b> haciendo <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <p>Cabe recordarle que, los 12 proyectos de FH fueron remitidos a su correo el 17/09/21 y también 
                                                puede acceder a ellos copiando y pegando en su navegador la siguiente ruta: https://acortar.link/FJ5h6R </p>
                                                <p>Gracias por su valio tiempo.</p>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Dirección de Análisis de Mercado - DAMER<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                    }
                }
                if (reenviar == 1)
                {
                    if (cuestionario.C_Cuestionario.Substring(0, 24) == "ENCUESTA DE SATISFACCIÓN")
                    {
                        response = @"
	                    <tr>
		                    <td class=""innerpadding borderbottom"">
			                    <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
				                    <tbody>
					                    <tr><td class=""h3"">Estimado usuario:</td></tr>
					                    " + tituloDestinatario + @"
					                    <tr>
						                    <td class=""bodycopy"">
							                    <p>
							                    De parte de la Central de Compras Públicas – PERÚ COMPRAS, queremos saludarte y a su vez recordarte 
							                    que la Encuesta de Satisfacción aún no ha sido respondida. Por lo cual, se le solicita responderlo 
							                    lo más pronto posible, recuerda que tu opinión es muy importante para nosotros, porque nos permitirá 
							                    medir el grado de satisfacción de los Catálogos Electrónicos de Acuerdos Marco respecto a la operatividad 
							                    de los Catálogos Electrónicos para el periodo de enero a diciembre del año 2021 e identificar oportunidad 
							                    de mejoras de este servicio. La encuesta le tomará unos minutos y es totalmente confidencial.
							                    </p>
							                    <p>Para responder la encuesta, haga clic en el siguiente enlace: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span> 
							                    o copie y pegue la siguiente dirección URL en su navegador de internet:</p>
							                    <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
							                    <p>Muchas gracias por su tiempo, estaremos atentos a su correo.</p>
							                    <br />
						                    </td>
					                    </tr>
					                    <tr>
						                    <td class=""firma"">
							                    <hr />
							                    Dirección de Análisis de Mercado - DAMER<br />
							                    Central de Compras Públicas – PERÚ COMPRAS<br />
							                    Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
							                    Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a><br />
						                    </td>
					                    </tr>
				                    </tbody>
			                    </table>
		                    </td>
	                    </tr>
	                    ";
                    }
                    else
                    {
                        response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado/a usuario/a:</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Reciba el cordial saludo de la Central de Compras Públicas – PERÚ COMPRAS. Mediante el 
                                                presente mensaje le reiteramos nuestra solicitud de responder la encuesta sobre el <b>“Impacto 
                                                de la aprobación de Fichas de Homologación en los Catálogos Electrónicos de computadoras”</b>, 
                                                haciendo <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <p>Cabe recordarle que, los 12 proyectos de Fichas de Homologación (que requiere revisar para el 
                                                desarrollo de la encuesta) fueron remitidos a su correo el 17/09/21 y también puede acceder a 
                                                ellos copiando y pegando en su navegador la siguiente ruta: https://acortar.link/FJ5h6R </p>
                                                <p>Gracias por su valioso tiempo!</p>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Dirección de Análisis de Mercado - DAMER<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a><br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                    }
                }
            }
            else if (tipo_area == 2) // Subasta Inversa, en local es 3
            {
                tituloDestinatario = @"<tr><td class=""h2"">" + nombre_completo + @"</td></tr>
                                        <tr><td class=""h4"">" + razon_social + @"</td></tr>";
                if (tipo_publico == 3)
                {
                    tituloDestinatario = @"<tr><td class=""h4"">" + razon_social + @"</td></tr>";
                }
                if (reenviar == 0)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Reciba el cordial saludo del Organismo Supervisor de las Contrataciones de Estado – OSCE y la 
                                                Central de Compras Públicas - PERÚ COMPRAS. Mediante el presente mensaje lo invitamos a responder
                                                la Encuesta de Satisfacción de Subasta Inversa Electrónica (en base a su experiencia durante el año 2021).
                                                </p>
                                                <p>Haga <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span> para continuar con la encuesta
                                                 o copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <P>Ayudanos a mejorar nuestros servicios, respondiendo toda la encuesta. La información que nos proporcione 
                                                será totalmente confidencial. Si tiene alguna consulta sobre la presente encuesta, escribanos a <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a></p>
                                                ¡Gracias por su valioso tiempo!<br /> 
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
                if (reenviar == 1)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Reciba el cordial saludo del Organismo Supervisor de las Contrataciones de Estado – OSCE y la 
                                                Central de Compras Públicas - PERÚ COMPRAS. Mediante el presente mensaje le reiteramos nuestra solicitud 
                                                a responder la Encuesta de Satisfacción de Subasta Inversa Electrónica (en base 
                                                a su experiencia durante el 2021).
                                                </p>
                                                <p>Haga <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span> para continuar con la encuesta
                                                 o copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <P>Ayudanos a mejorar nuestros servicios, respondiendo toda la encuesta. La información que nos proporcione 
                                                será totalmente confidencial. Si tiene alguna consulta sobre la presente encuesta, escribanos a <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a></p>
                                                ¡Gracias por su valioso tiempo!<br /> 
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
            }
            else if (tipo_area == 6) // Dirección de Compra Corporativa, en local es 6
            {
                tituloDestinatario = @"<tr><td class=""h2"">" + nombre_completo + @"</td></tr>
                                        <tr><td class=""h4"">" + razon_social + @"</td></tr>";

                if (reenviar == 0)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Como parte del continuo esfuerzo por mejorar, la Central de Compras Públicas – PERÚ COMPRAS 
                                                lo ha seleccionado para el desarrollo de una encuesta que busca identificar los factores que 
                                                favorecen la estrategia de Compra Agregada. Su opinión es muy importante para nosotros y le 
                                                aseguramos que toda la información que nos proporcione será confidencial.
                                                </p>
                                                <p>Haz clic en este enlace para continuar con la encuesta: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copia y pega la siguiente dirección URL en tu navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                Muchas gracias por tu tiempo.<br /> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Dirección de Análisis de Mercado - DAMER<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a><br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
                if (reenviar == 1)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Previo cordial saludo, de parte de la Central de Compras Públicas – PERÚ COMPRAS se le 
                                                recuerda que usted tiene pendiente el envío de la “Encuesta sobre los factores que favorecen la 
                                                Compra Agregada”. Recuerde que su opinión es muy importante para nosotros y le aseguramos 
                                                que toda la información que nos proporcione será confidencial.
                                                </p>
                                                <p>Haz clic en este enlace para continuar con la encuesta: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copia y pega la siguiente dirección URL en tu navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                Muchas gracias por tu tiempo.<br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Dirección de Análisis de Mercado - DAMER<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:damer_encuesta@perucompras.gob.pe"">damer_encuesta@perucompras.gob.pe</a><br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
            }
            else if (tipo_area == 7) // Dirección de Compra Corporativa, en local es 6
            {
                tituloDestinatario = @"<tr><td class=""h2"">" + nombre_completo + @"</td></tr>
                                        <tr><td class=""h4"">" + razon_social + @"</td></tr>";

                if (reenviar == 0 || reenviar == 1)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                            <td class=""bodycopy2"">
                                                <p>
                                                Estamos realizando una encuesta para conocer su satisfacción de la charla brindada.
                                                </p>
                                                <p>
                                                Ayúdanos respondiendo las preguntas.
                                                </p>
                                                <p>
                                                ¡Queremos seguir mejorando para ti!
                                                </p>
                                                <p><span class=""button""><a href=""" + enlace + @""">EMPEZAR ENCUESTA</a></span></p>
                                                <br /> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Oficina de Comunicaciones - OC<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:comunicaciones@perucompras.gob.pe"">comunicaciones@perucompras.gob.pe</a><br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
            }
            else
            {
                if (reenviar == 0)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) Usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Con la finalidad de conocer la situación y características actuales del Sistema Control Interno implementado al 2018, 
                                                y determinar los aspectos que requieren ser incorporados y/o reforzados, la Central de Compras Públicas – PERÚ COMPRAS, 
                                                lo ha seleccionado para el desarrollo de una encuesta, que busca medir el grado de conocimiento sobre la implementación
                                                del Sistema de Control Interno en la entidad. La encuesta le tomará unos minutos, su opinión es muy importante para nosotros. 
                                                Le aseguramos que toda la información que nos proporcione será confidencial.
                                                </p>
                                                <p>Haga clic en este enlace para continuar con la encuesta: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <p>Muchas gracias por su tiempo.</p>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Oficina de Planeamiento y Presupuesto - OPP<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:opp.encuesta@perucompras.gob.pe"">opp.encuesta@perucompras.gob.pe</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }

                if (reenviar == 1)
                {
                    response = @"
                        <tr>
                            <td class=""innerpadding borderbottom"">
                                <table style=""width:800px"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                    <tbody>
                                        <tr><td class=""h3"">Estimado(a) Usuario(a):</td></tr>
                                        " + tituloDestinatario + @"
                                        <tr>
                                            <td class=""bodycopy"">
                                                <p>
                                                Previo cordial saludo, de parte de la Central de Compras Públicas – PERÚ COMPRAS, se le 
                                                recuerda que la Encuesta de Conocimiento enviada por este medio aún no ha sido enviada. 
                                                Razón por la cual, se le solicita responder lo más pronto posible. Recuerde que su opinión es
                                                importante para nosotros; ya que, nos permitirá conocer la situación y características actuales
                                                del Sistema Control Interno implementado al 2018, y determinar los aspectos que requieren ser 
                                                incorporados y/o reforzados. Le aseguramos que toda la información que nos proporcione será confidencial.
                                                </p>
                                                <p>Haga clic en este enlace para continuar con la encuesta: <span class=""enlaceC""><a href=""" + enlace + @""">Clic aqu&iacute;</a></span></p>
                                                <p>O copie y pegue la siguiente dirección URL en su navegador de internet:</p>
                                                <p class=""enlaceC""><a href=""" + enlace + @""">" + enlace + @"</a></p>
                                                <p>Muchas gracias por su tiempo.</p>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class=""firma"">
                                                <hr />
                                                Oficina de Planeamiento y Presupuesto - OPP<br />
                                                Central de Compras Públicas – PERÚ COMPRAS<br />
                                                Avenida República de Panamá N° 3629 - San Isidro, Lima<br />
                                                Consultas al correo: <a href=""mailto:opp.encuesta@perucompras.gob.pe"">opp.encuesta@perucompras.gob.pe</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        ";
                }
            }


            return response;
        }

        private string pie(int tipo_area)
        {
            string ImagenesEncuesta = ConfigurationManager.AppSettings["ImagenesEncuesta"].ToString();
            string trImagen = "";

            if (tipo_area == 2)
            {
                ImagenesEncuesta = ImagenesEncuesta + "logos_cierre.jpg";
                trImagen = @"<tr>
                                    <td>
                                        <img style = ""width: 860px"" width = ""860"" class=""fix"" src=""" + ImagenesEncuesta + @""">
                                    </td>
                                </tr>";
            }

            return trImagen + @"
                    <tr>
                        <td class=""footer"" style=""width:800px !important"" bgcolor=""#df0209"">
                            <table style=""width:800px !important"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                <tbody>
                                    <tr>
                                        <td align=""center"" class=""footercopy"">
                                            ® Encuestas - Perú Compras<br>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                ";
        }

        [HttpGet]
        public async Task<JsonResult> ReenviarInvitacion(int ID_Usuario, int ID_Cuestionario, int ID_Correo, string pTipo, string pUbigeo, int pEstadoE)
        {
            string dnsSetting = ConfigurationManager.AppSettings["DNS_Server"].ToString();
            string registrosEnviar = ConfigurationManager.AppSettings["registros_enviar"].ToString();
            string sRpta = "OK";

            List<T_AccesoInvitacion> lstInvitacion = DB.ListarAccesoInvitacionesEnviadas(ID_Usuario, ID_Cuestionario, ID_Correo, 0, pTipo, pUbigeo, pEstadoE);

            if (lstInvitacion.Count <= Int32.Parse(registrosEnviar))
            {
                sRpta = ReenviarInvitacionR(ID_Usuario, ID_Cuestionario, ID_Correo, Int32.Parse(registrosEnviar), pTipo, pUbigeo, pEstadoE);
            }
            else
            {
                int bucle = (lstInvitacion.Count / Int32.Parse(registrosEnviar)) + 1;

                for (int i = 0; i <= bucle; i++)
                {
                    sRpta = ReenviarInvitacionR(ID_Usuario, ID_Cuestionario, ID_Correo, Int32.Parse(registrosEnviar), pTipo, pUbigeo, pEstadoE);
                    Thread.Sleep(3000);
                }
            }
            return Json(sRpta, JsonRequestBehavior.AllowGet);
        }

        public string ReenviarInvitacionR(int ID_Usuario, int ID_Cuestionario, int ID_Correo, int reg, string pTipo, string pUbigeo, int pEstadoE)
        {
            string Usuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                Usuario = TempData["Usuario"].ToString();
            }

            string dnsSetting = ConfigurationManager.AppSettings["DNS_Server"].ToString();
            string sRpta = "OK";

            List<T_AccesoInvitacion> lstInvitacion = DB.ListarAccesoInvitacionesEnviadas(ID_Usuario, ID_Cuestionario, ID_Correo, reg, pTipo, pUbigeo, pEstadoE);
            T_Cuestionario cuestionario = DB.T_Cuestionario.Find(ID_Cuestionario);

            if (lstInvitacion.Count > 0)
            {
                foreach (var oInvita in lstInvitacion)
                {
                    //Construye Correo
                    string sToken = oInvita.C_Token;
                    string sRazonSocial = oInvita.C_RazonSocial;
                    string sNombre = oInvita.C_NombreCompleto;
                    string EMail = oInvita.C_Correo;
                    int iTipoA = oInvita.ID_Area;
                    int iTipoP = oInvita.ID_Publico;
                    string strLink = "https://" + dnsSetting + "/Accesos/token?token=" + sToken;

                    string strEmailPart = EMail.Substring(0, 5) + "**********";

                    string body = generaMensaje(sRazonSocial, sNombre, strLink, iTipoA, iTipoP, 1, cuestionario);
                    sRpta = EnviarCorreo(EMail, body, Usuario, oInvita.ID_Invitacion, 9);
                }
            }
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");

            return sRpta;
        }
    }
}