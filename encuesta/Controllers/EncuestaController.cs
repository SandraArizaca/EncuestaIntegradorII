using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using encuesta.Contexto;
using encuesta.Models;
using encuesta.FIlters;
using System.Configuration;

namespace encuesta.Controllers
{
    public class EncuestaController : Controller
    {
        private CTX_Encuesta DB = new CTX_Encuesta();

        public int SqlFlag = 0; //0=Normal, 1=Aviso, 2=Error
        public string SqlMsg = string.Empty;
        private string cVista;

        public DbSet<T_Usuario> T_Usuario { set; get; }

        public ActionResult Index()
        {
            int idUsuario = 0;
            string token = "";

            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }
            if (TempData.ContainsKey("Token_Encuesta_Publico"))
            {
                token = TempData.Peek("Token_Encuesta_Publico").ToString();
            }
            ViewBag.ID_Usuario = idUsuario;

            if (idUsuario != 0)
            {
                //Verifico si el usuario tiene Encuestas pendientes
                List<T_Encuesta> lstEncuestas = new List<T_Encuesta>();
                lstEncuestas = DB.T_Encuesta.Where(item => item.ID_Usuario == idUsuario && item.ID_Estado < 12).ToList();

                int nCuestionario = 0;
                int nEncuesta = 0;
                if (lstEncuestas.Count > 0)
                {
                    nCuestionario = lstEncuestas.ElementAt(0).ID_Cuestionario;
                    nEncuesta = lstEncuestas.ElementAt(0).ID_Encuesta;
                    
                    List<T_Configuracion> lst = new List<T_Configuracion>();
                    lst = DB.ListConfigDetalleEncuesta(nCuestionario, nEncuesta, token);
                    lst = lst.Where(item => item.ID_Pregunta != 0 && item.ID_Alternativa == 0).OrderBy(item => item.N_OrdenPreg).ToList();

                    if (lst.Count > 0) {
                        nCuestionario = lst.ElementAt(0).ID_Cuestionario;
                        T_Cuestionario obj = DB.T_Cuestionario.Find(nCuestionario);
                        lstEncuestas = DB.T_Encuesta.Where(item => item.ID_Usuario == idUsuario && item.ID_Estado < 12 && item.ID_Cuestionario == nCuestionario).ToList();

                        if (lst.Count > 0)
                        {
                            ViewBag.ID_Encuesta = lstEncuestas.ElementAt(0).ID_Encuesta;
                        }
                        else
                        {
                            ViewBag.ID_Encuesta = 0;
                        }
                        ViewBag.ID_Cuestionario = obj.ID_Cuestionario;
                        ViewBag.C_Titulo = obj.C_Cuestionario;
                        ViewBag.N_Preguntas = obj.N_NroPreguntas;
                        ViewBag.N_Pregunta = lst.ElementAt(0).ID_Pregunta;
                        ViewBag.N_Orden = lst.ElementAt(0).N_OrdenPreg;
                        ViewBag.N_FinalizoEncuesta = 0;
                        ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
                    }
                    else
                    {
                        return RedirectToAction("Msg_noTieneEncuestas", "Encuesta");
                    }

                    return View();
                }
                else
                {
                    return RedirectToAction("Msg_noTieneEncuestas", "Encuesta");
                }
                
                //List<T_Invitacion> lstInvitacion = new List<T_Invitacion>();
                //lstInvitacion = DB.T_Invitacion.Where(item => item.ID_Usuario == idUsuario && item.ID_Estado != 8).ToList();

                //int nCuestionario = 0;
                //int nInvitacion = 0;
                //if (lstInvitacion.Count > 0)
                //{
                //    nCuestionario = lstInvitacion.ElementAt(0).N_Cuestionario;
                //    nInvitacion = lstInvitacion.ElementAt(0).ID_Invitacion;

                //    T_Cuestionario obj = DB.T_Cuestionario.Find(nCuestionario);

                //    //NO es vista previa del cuestionario
                //    ViewBag.IsVP = 0;

                //    ViewBag.ID_Invitacion = nInvitacion;
                //    ViewBag.ID_Cuestionario = obj.ID_Cuestionario;

                //    ViewBag.C_Titulo = obj.C_Titulo;
                //    ViewBag.C_Presentacion = obj.C_Presentacion;
                //    ViewBag.C_Metodo = obj.C_Metodo;
                //    ViewBag.N_Preguntas = obj.N_Preguntas;

                //    List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
                //    List<TMP_Pregunta> lstPregunta = new List<TMP_Pregunta>();

                //    string strsql = "SELECT ID_Encuesta, N_Invitacion, N_Pregunta, 0 AS ID_Pregunta, N_Orden, T_Encuesta.N_Estado"
                //        + " FROM t_encuesta INNER JOIN t_encuestadetalle ON t_encuesta.id_encuesta=t_encuestadetalle.n_encuesta"
                //        + " WHERE n_invitacion = " + nInvitacion + " ORDER BY n_pregunta desc";

                //    string strsql2 = "SELECT ID_Pregunta,N_Orden"
                //         + " FROM T_Seccion"
                //         + " INNER JOIN T_Pregunta ON T_Seccion.ID_Seccion = T_Pregunta.N_Seccion"
                //         + " WHERE N_Cuestionario = " + obj.ID_Cuestionario
                //         + " ORDER BY N_Orden";

                //    lstEncuesta = DB.T_Encuesta.SqlQuery(strsql).ToList();
                //    int datos = lstEncuesta.Count;
                //    if (datos > 0)
                //    {
                //        ViewBag.N_Pregunta = lstEncuesta.ElementAt(0).N_Pregunta + 1;
                //        ViewBag.N_Orden = lstEncuesta.ElementAt(0).N_Orden + 1;
                //    }
                //    else
                //    {
                //        lstPregunta = DB.TMP_Pregunta.SqlQuery(strsql2).ToList();
                //        ViewBag.N_Pregunta = lstPregunta.ElementAt(0).ID_Pregunta;
                //        ViewBag.N_Orden = lstPregunta.ElementAt(0).N_Orden;
                //    }


                //    return View();
                //}
                //else
                //{
                //    return RedirectToAction("Msg_noTieneEncuestas", "Encuesta");
                //}
            }
            else
            {
                //return RedirectToAction("Index", "Accesos");
                return RedirectToAction("Msg_noTieneEncuestas", "Encuesta");
            }
        }
        [TokenFilter]
        public ActionResult VistaPrevia(int nCuestionario)
        {
            int idUsuario = 0;
            string token = "";

            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }
            //if (TempData.ContainsKey("Token"))
            //{
            //    token = TempData.Peek("Token").ToString();
            //}

            ViewBag.ID_Usuario = idUsuario;

            if (idUsuario != 0)
            {
                List<T_Invitacion> lstInvitacion = new List<T_Invitacion>();
                List<T_Encuesta> lstEncuestas = new List<T_Encuesta>();
                lstEncuestas = DB.T_Encuesta.Where(item => item.ID_Usuario == idUsuario && item.ID_Estado < 12 && item.ID_Cuestionario == nCuestionario).ToList();
                int nEncuesta = 0;
                if (lstEncuestas.Count > 0)
                {
                    nEncuesta = lstEncuestas.ElementAt(0).ID_Encuesta;
                }
                else
                {
                    lstInvitacion = DB.T_Invitacion.Where(item => item.ID_Usuario == idUsuario && item.ID_Cuestionario == nCuestionario && item.ID_Estado == 8).ToList();
                    if (lstInvitacion.Count() > 0)
                    {
                        nEncuesta = 0;
                    }
                    else
                    {
                        nEncuesta = DB.AgregarInvitacionVP(nCuestionario, idUsuario);
                    }
                }

                if (nEncuesta > 0)
                {
                    ViewBag.ID_Encuesta = nEncuesta;

                    T_Cuestionario obj = DB.T_Cuestionario.Find(nCuestionario);

                    ViewBag.ID_Cuestionario = obj.ID_Cuestionario;
                    ViewBag.C_Titulo = obj.C_Cuestionario;
                    ViewBag.C_Presentacion = obj.C_Presentacion;
                    ViewBag.C_Metodo = obj.C_Resumen;
                    ViewBag.N_Preguntas = obj.N_NroPreguntas;

                    List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
                    List<T_Configuracion> lstPregunta = new List<T_Configuracion>();

                    //string strsql = "SELECT ID_Encuesta, N_Invitacion, N_Pregunta, 0 AS ID_Pregunta, N_Orden, T_Encuesta.N_Estado"
                    //    + " FROM t_encuesta INNER JOIN t_encuestadetalle ON t_encuesta.id_encuesta=t_encuestadetalle.n_encuesta"
                    //    + " WHERE n_invitacion = " + nInvitacion + " ORDER BY n_pregunta desc";

                    //string strsql2 = "SELECT ID_Pregunta,N_Orden"
                    //        + " FROM T_Seccion"
                    //        + " INNER JOIN T_Pregunta ON T_Seccion.ID_Seccion = T_Pregunta.N_Seccion"
                    //        + " WHERE N_Cuestionario = " + obj.ID_Cuestionario
                    //        + " ORDER BY N_Orden";

                    lstPregunta = DB.ListConfigDetalleEncuesta(nCuestionario, nEncuesta, token).ToList();
                    lstPregunta = lstPregunta.Where(item => item.ID_Pregunta != 0 && item.ID_Alternativa == 0).OrderBy(item => item.N_OrdenPreg).ToList();

                    if(lstPregunta.Count > 0)
                    {
                        ViewBag.N_Pregunta = lstPregunta.ElementAt(0).ID_Pregunta;
                        ViewBag.N_Orden = lstPregunta.ElementAt(0).N_OrdenPreg;
                        ViewBag.N_FinalizoEncuesta = 0;
                        ViewBag.ID_Seccion = lstPregunta.ElementAt(0).ID_Seccion;
                    }
                    else
                    {
                        return RedirectToAction("Msg_noTieneEncuestas", "Encuesta");
                    }
                    
                    //Es una Vista Previa del cuestionario
                    ViewBag.IsVP = 1;

                    return View("Index");
                }
                else
                {
                    return View("Msg_yaFinalizoEncuesta");
                }


            }
            else
            {
                return View("Index");
            }

            //string cUsuario = "";
            //if (TempData.ContainsKey("Usuario"))
            //{
            //    cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            //}

            //ViewBag.C_Usuario = cUsuario;

            //if (cUsuario != "")
            //{
            //    List<T_Invitacion> lstInvitacion = new List<T_Invitacion>();
            //    List<T_Invitacion> lstInvita = new List<T_Invitacion>();
            //    lstInvitacion = DB.T_Invitacion.Where(item => item.N_Usuario == cUsuario && item.N_Cuestionario == nCuestionario && item.N_Estado != 7).ToList();

            //    int nInvitacion = 0;

            //    if (lstInvitacion.Count() > 0)
            //    {
            //        nInvitacion = lstInvitacion.ElementAt(0).ID_Invitacion;
            //    }
            //    else
            //    {
            //        lstInvita = DB.T_Invitacion.Where(item => item.N_Usuario == cUsuario && item.N_Cuestionario == nCuestionario && item.N_Estado == 7).ToList();
            //        if (lstInvita.Count() > 0)
            //        {
            //            nInvitacion = 0;
            //        }
            //        else
            //        {
            //            nInvitacion = DB.AgregarInvitacionVP(nCuestionario, cUsuario);
            //        }
            //    }

            //    if (nInvitacion > 0)
            //    {
            //        ViewBag.ID_Invitacion = nInvitacion;

            //        T_Cuestionario obj = DB.T_Cuestionario.Find(nCuestionario);

            //        ViewBag.ID_Cuestionario = obj.ID_Cuestionario;
            //        ViewBag.C_Titulo = obj.C_Titulo;
            //        ViewBag.C_Presentacion = obj.C_Presentacion;
            //        ViewBag.C_Metodo = obj.C_Metodo;
            //        ViewBag.N_Preguntas = obj.N_Preguntas;

            //        List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
            //        List<TMP_Pregunta> lstPregunta = new List<TMP_Pregunta>();

            //        string strsql = "SELECT ID_Encuesta, N_Invitacion, N_Pregunta, 0 AS ID_Pregunta, N_Orden, T_Encuesta.N_Estado"
            //            + " FROM t_encuesta INNER JOIN t_encuestadetalle ON t_encuesta.id_encuesta=t_encuestadetalle.n_encuesta"
            //            + " WHERE n_invitacion = " + nInvitacion + " ORDER BY n_pregunta desc";

            //        string strsql2 = "SELECT ID_Pregunta,N_Orden"
            //                + " FROM T_Seccion"
            //                + " INNER JOIN T_Pregunta ON T_Seccion.ID_Seccion = T_Pregunta.N_Seccion"
            //                + " WHERE N_Cuestionario = " + obj.ID_Cuestionario
            //                + " ORDER BY N_Orden";

            //        lstEncuesta = DB.T_Encuesta.SqlQuery(strsql).ToList();

            //        int datos = lstEncuesta.Count;
            //        if (datos > 0)
            //        {
            //            ViewBag.N_Pregunta = lstEncuesta.ElementAt(0).N_Pregunta + 1;
            //            ViewBag.N_Orden = lstEncuesta.ElementAt(0).N_Orden + 1;
            //            ViewBag.N_FinalizoEncuesta = lstEncuesta.ElementAt(0).N_Estado;
            //        }
            //        else
            //        {
            //            lstPregunta = DB.TMP_Pregunta.SqlQuery(strsql2).ToList();
            //            ViewBag.N_Pregunta = lstPregunta.ElementAt(0).ID_Pregunta;
            //            ViewBag.N_Orden = lstPregunta.ElementAt(0).N_Orden;
            //            ViewBag.N_FinalizoEncuesta = 0;
            //        }

            //        //Es una Vista Previa del cuestionario
            //        ViewBag.IsVP = 1;

            //        return View("Index");
            //    }
            //    else
            //    {
            //        return View("Msg_yaFinalizoEncuesta");
            //    }
            //}
            //else
            //{
            //    return View("Index");
            //}
        }
        public ActionResult Pregunta(int pIDCuestionario, int pNSeccion, int pNPregunta, int pIDEncuesta)
        {
            string token = "";
            //if (TempData.ContainsKey("Token"))
            //{
            //    token = TempData.Peek("Token").ToString();
            //}
            string ImagenesEncuesta = ConfigurationManager.AppSettings["ImagenesEncuesta"].ToString();
            ViewBag.ImagenesEncuesta = "/Img/Logo.png"; //ImagenesEncuesta+ "msg_encuesta_ok.png";
            List<T_Configuracion> lstPregunta = new List<T_Configuracion>();
            lstPregunta = DB.ListConfigDetalleEncuesta(pIDCuestionario, pIDEncuesta, token).ToList();
            lstPregunta = lstPregunta.Where(item => item.ID_Seccion == pNSeccion && item.ID_Pregunta == pNPregunta).ToList();

            if (lstPregunta.Count > 1)
            {
                lstPregunta = lstPregunta.Where(item => item.ID_Seccion == pNSeccion && item.ID_Pregunta == pNPregunta && item.ID_Alternativa != 0).OrderBy(item => item.N_Orden).ThenBy(item => item.N_IntervaloE).ToList();
            }

            //string cUsuario = "";
            //if (TempData.ContainsKey("Usuario"))
            //{
            //    cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            //}
            //ViewBag.C_Usuario = cUsuario;

            //if (!IsVP.HasValue) { IsVP = 0; }

            //List<T_Seccion> lst = DB.T_Seccion.Where(x => x.N_Cuestionario == pIDCuestionario).ToList();
            //T_Seccion obj = lst.ElementAt(0);
            //ViewBag.ID_Seccion = obj.ID_Seccion;
            //ViewBag.C_Seccion = obj.C_Seccion;

            //List<TMP_Encuesta> lstEncuesta = new List<TMP_Encuesta>();

            //string strsql = "SELECT ID_Seccion,N_Cuestionario,C_Seccion,ID_Pregunta,0 AS N_Pregunta,N_Orden,C_Pregunta,C_Presentacion,C_Metodo,"
            //     + " ISNULL(T_Alternativa.ID_Alternativa, 0) AS ID_Alternativa, ISNULL(C_Alternativa,'') AS C_Alternativa, N_TipoPregunta, N_Likert,"
            //     + " ISNULL(T_Alternativa.B_TieneComentario, 0) AS B_TieneComentario,  0 AS N_PreguntaBifurca,"
            //     + " ISNULL(T_Alternativa.N_Peso, 0) AS N_Peso, ISNULL(T_Alternativa.B_FinalizaEncuesta, 0) AS B_FinalizaEncuesta,"
            //     + " ISNULL(B_NuevoOrden,0) AS B_NuevoOrden,ISNULL(B_SoloEstaOpcionNuevoOrden,0) AS B_SoloEstaOpcionNuevoOrden, ISNULL(B_LikertComentario,0) AS B_LikertComentario"
            //     + " FROM T_Seccion"
            //     + " INNER JOIN T_Cuestionario ON T_Seccion.N_Cuestionario = T_Cuestionario.ID_Cuestionario"
            //     + " INNER JOIN T_Pregunta ON T_Seccion.ID_Seccion = T_Pregunta.N_Seccion"
            //     + " LEFT JOIN T_Alternativa ON T_Pregunta.ID_Pregunta = T_Alternativa.N_Pregunta"
            //     + " WHERE N_Cuestionario = " + pIDCuestionario + " AND ID_Pregunta = " + pNPregunta
            //     + " ORDER BY N_Orden";

            //lstEncuesta = DB.TMP_Encuesta.SqlQuery(strsql).ToList();
            int N_TipoPregunta = 0;
            //int N_Likert = 0;
            string C_Seccion = "";
            int N_Pregunta = 0;
            int N_Orden = 0;
            if (lstPregunta.Count > 0)
            {
                N_TipoPregunta = lstPregunta.ElementAt(0).T_Pregunta.ID_TipoPregunta;
            //    if (N_TipoPregunta == 3)
            //    {
            //        N_Likert = lstEncuesta.ElementAt(0).N_Likert.Value;
            //        List<T_LikertValoracion> lstLikertValoracion = DB.T_LikertValoracion.Where(x => x.N_Likert == N_Likert).ToList();
            //        TempData["LikertValoracion"] = lstLikertValoracion;
            //    }
                C_Seccion = lstPregunta.ElementAt(0).T_Seccion.C_Seccion;
                N_Orden = lstPregunta.ElementAt(0).N_OrdenPreg.Value;
                N_Pregunta = lstPregunta.ElementAt(0).ID_Pregunta;

                ViewBag.C_Presentacion = lstPregunta.ElementAt(0).T_Cuestionario.C_Presentacion;
                ViewBag.C_Metodo = lstPregunta.ElementAt(0).T_Cuestionario.C_Resumen;
                ViewBag.ID_Seccion = lstPregunta.ElementAt(0).ID_Seccion;

                ViewBag.C_Seccion = C_Seccion;
                ViewBag.N_Orden = N_Orden;
                ViewBag.N_Pregunta = N_Pregunta;
                ViewBag.N_PreguntaSiguiente = N_Orden + 1;

                if (N_Orden == 1)
                {
                    ViewBag.N_PreguntaAnterior = 1;
                }
                else
                {
                    ViewBag.N_PreguntaAnterior = N_Orden - 1;
                }
            }
            else
            {
                N_TipoPregunta = 100;
                N_Pregunta = 0;
                ViewBag.N_Pregunta = 0;
                ViewBag.N_PreguntaAnterior = 0;
            }

            switch (N_TipoPregunta)
            {
                case 2:
                    cVista = "PSimple";
                    break;
                case 3:
                    cVista = "PMultiple";
                    break;
                case 4:
                    cVista = "PLikert";
                    break;
                case 5:
                    cVista = "PComentario";
                    break;
                case 100:
                    cVista = "MsgFinal";
                    break;
            }
            return PartialView(cVista, lstPregunta);
        }
        
        [HttpGet]
        [TokenEncuestaPublicoFilter]
        public JsonResult Pregunta_anterior(int pEncuesta, int pSeccion, int pPregunta)
        {
            //string cUsuario = "";
            //if (TempData.ContainsKey("Usuario"))
            //{
            //    cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            //}
            //ViewBag.C_Usuario = cUsuario;

            List<T_DetalleEncuesta> lst = new List<T_DetalleEncuesta>();
            //List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
            //string strsql_a = "SELECT ID_Encuesta,N_Invitacion,0 AS N_Pregunta,0 AS ID_Pregunta,0 AS N_Orden,0 AS N_Estado FROM T_Encuesta WHERE N_Invitacion=" + pInvitacion;
            //lstEncuesta = DB.T_Encuesta.SqlQuery(strsql_a).ToList();
            //if (lstEncuesta.Count > 0)
            //{
            //    N_Encuesta = lstEncuesta.ElementAt(0).ID_Encuesta;
            //}
            //else
            //{
            //    N_Encuesta = 0;
            //}
            //List<T_EncuestaDetalle> lstDetalle = new List<T_EncuestaDetalle>();
            //string strsql = "SELECT ID_Respuesta,N_Encuesta,N_Pregunta,0 AS N_Alternativa FROM T_EncuestaDetalle WHERE N_Encuesta=" + N_Encuesta + " ORDER BY N_Pregunta DESC";
            //lstDetalle = DB.T_EncuestaDetalle.SqlQuery(strsql).ToList();
            //int reg = lstDetalle.Count;
            //if (reg > 0)
            //{
            //    N_Pregunta = lstDetalle.ElementAt(0).N_Pregunta;
            //    int res;
            //    res = DB.EliminaAlternativa(N_Encuesta, N_Pregunta);
            //}
            //else
            //{
            //    N_Pregunta = nPregunta;
            //}
            lst = DB.ConsultaAlternativa(pEncuesta, pSeccion, pPregunta);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [TokenEncuestaPublicoFilter]
        public JsonResult Registra_alternativa(int pEncuesta, int pSeccion, int pPregunta, int pAlternativa, string pComentario, int? pIntervalo)
        {
            int idUsuario = 0;
            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            if (!pIntervalo.HasValue) { pIntervalo = 0; }
            List<T_DetalleEncuesta> lst = new List<T_DetalleEncuesta>();
            //int res;
            lst = DB.RegistraAlternativa(pEncuesta, pSeccion, pPregunta, pAlternativa, pComentario, pIntervalo, idUsuario);
            //ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
            //List<T_Configuracion> lstPregunta = new List<T_Configuracion>();
            //List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
            //lstEncuesta = DB.T_Encuesta.Where(item => item.ID_Encuesta == pEncuesta).ToList();

            //var id = lstEncuesta.ElementAt(0).ID_Cuestionario;
            //lstPregunta = DB.ListConfigDetalleEncuesta(id, pEncuesta).ToList();
            //lstPregunta = lstPregunta.Where(item => item.ID_Pregunta != res && item.ID_AlternativaR == 0 && item.ID_Pregunta != 0).OrderBy(item => item.N_Orden).ToList();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [TokenEncuestaPublicoFilter]
        public JsonResult Registra_alternativas(List<T_DetalleEncuesta> pDetalleEncuesta)
        {
            int idUsuario = 0;
            if (TempData.ContainsKey("IDUsuario"))
            {
                idUsuario = Convert.ToInt32(TempData.Peek("IDUsuario"));
            }

            int res = EliminaAlternativa(pDetalleEncuesta.ElementAt(0).ID_Encuesta, pDetalleEncuesta.ElementAt(0).ID_Seccion, pDetalleEncuesta.ElementAt(0).ID_Pregunta);
            List<T_DetalleEncuesta> lst = new List<T_DetalleEncuesta>();
            //int res;
            for (int i= 0; i < pDetalleEncuesta.Count; i++) 
            {
                var cComentario = pDetalleEncuesta.ElementAt(i).C_Comentario;
                if(cComentario == null) { cComentario = ""; }
                lst = DB.RegistraAlternativa(pDetalleEncuesta.ElementAt(i).ID_Encuesta, pDetalleEncuesta.ElementAt(i).ID_Seccion, pDetalleEncuesta.ElementAt(i).ID_Pregunta, pDetalleEncuesta.ElementAt(i).ID_Alternativa, cComentario, pDetalleEncuesta.ElementAt(i).N_Intervalo, idUsuario);
            }
            
            //ViewBag.ID_Seccion = lst.ElementAt(0).ID_Seccion;
            //List<T_Configuracion> lstPregunta = new List<T_Configuracion>();
            //List<T_Encuesta> lstEncuesta = new List<T_Encuesta>();
            //lstEncuesta = DB.T_Encuesta.Where(item => item.ID_Encuesta == pEncuesta).ToList();

            //var id = lstEncuesta.ElementAt(0).ID_Cuestionario;
            //lstPregunta = DB.ListConfigDetalleEncuesta(id, pEncuesta).ToList();
            //lstPregunta = lstPregunta.Where(item => item.ID_Pregunta != res && item.ID_AlternativaR == 0 && item.ID_Pregunta != 0).OrderBy(item => item.N_Orden).ToList();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [TokenEncuestaPublicoFilter]
        public int EliminaAlternativa(int pEncuesta, int pSeccion, int pPregunta)
        {
            int res;
            res = DB.EliminaAlternativa(pEncuesta, pSeccion, pPregunta);

            return res;
        }

        //public int Registra_usuario(string pRuc, string pDni, string pClave, string pEmail)
        //{
        //    int res;
        //    res = DB.RegistraUsuario(pRuc, pDni, pClave, pEmail);
        //    return res;
        //}

        //
        //public ActionResult Directorio()
        //{
        //string cUsuario = "";
        //if (TempData.ContainsKey("Usuario"))
        //{
        //    cUsuario = Convert.ToString(TempData.Peek("Usuario"));
        //}
        //ViewBag.C_Usuario = cUsuario;

        //List<T_Cuestionario> lst = new List<T_Cuestionario>();
        //string strSql = "SELECT ID_Cuestionario, C_Titulo, C_Presentacion, C_Metodo, N_Publico, N_Preguntas,"
        //    + " D_Inicio, D_Final,"
        //    + " C_MensajeInicial, C_MensajeFinal, c.N_Estado, e.C_Estado,"
        //    + " C_Publico"
        //    + " FROM T_Cuestionario AS c"
        //    + " INNER JOIN T_Publico AS p ON c.N_Publico=p.ID_Publico"
        //    + " INNER JOIN T_Estado AS e ON c.N_Estado=e.ID_Estado"
        //    + " ORDER BY C_Titulo";
        //lst = DB.T_Cuestionario.SqlQuery(strSql).ToList();

        //List<T_Area> lstArea = new List<T_Area>();
        //List<T_Publico> lstPublico = new List<T_Publico>();
        //lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
        //lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
        //ViewBag.area = lstArea;
        //ViewBag.publico = lstPublico;

        //ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
        //ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;

        //List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
        //lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Area == lstArea.ElementAt(0).ID_Area && item.ID_Publico == lstPublico.ElementAt(0).ID_Publico).ToList();
        //if (lstCuestionario.Count == 0) { lstCuestionario.Add(new T_Cuestionario() { ID_Cuestionario = 0, C_Cuestionario = "NO existen elementos a mostrar", N_NroPreguntas = 0 }); }

        //if (lstCuestionario.Count == 0)
        //{

        //}
        //else
        //{
        //    ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
        //    ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
        //}
        //ViewBag.N_Cuestionario = lstCuestionario.ElementAt(0).ID_Cuestionario;

        //    return View();
        //}
        [TokenEncuestaPublicoFilter]
        public ActionResult Msg_yaFinalizoEncuesta()
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            return View();
        }
        
        public ActionResult Msg_noTieneEncuestas()
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            return View();
        }
        [TokenEncuestaPublicoFilter]
        public ActionResult Msg_noTieneEncuestas2()
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            return View();
        }
        [TokenFilter]
        public ActionResult Directorio(int? N_Cuestionario)
        {
            if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();
            
            if (lstCuestionario.Count == 0)
            {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }

            ViewBag.N_Cuestionario = N_Cuestionario.Value;

            return View();

        }
        [TokenFilter]
        public ActionResult Directorio_datos(int? pCuestionario)
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            List<T_Directorio> lst = new List<T_Directorio>();
            lst = DB.ListDirectorio(pCuestionario.Value, 0, "All", "All", 0);
            ViewBag.N_Cuestionario = pCuestionario.Value;

            return PartialView(lst);
        }

        [TokenFilter]
        public ActionResult Invitacion(int? N_Cuestionario)
        {
            if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }

            ViewBag.C_Usuario = cUsuario;

            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();

            if (lstCuestionario.Count == 0)
            {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }

            ViewBag.N_Cuestionario = N_Cuestionario.Value;

            return View();
        }

        [TokenFilter]
        public ActionResult Invitacion_datos(int? pCuestionario)
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            List<T_Directorio> lst = new List<T_Directorio>();

            lst = DB.ListDirectorio(pCuestionario.Value, 1, "All", "All", 0);
            ViewBag.N_Cuestionario = pCuestionario.Value;

            return PartialView(lst);
        }

        [TokenFilter]
        public ActionResult Muestreo(int? pCuestionario)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }

            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario).ToList();

            if (lstCuestionario.Count == 0)
            {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }

            ViewBag.N_Cuestionario = pCuestionario.Value;

            return View();
        }

        [HttpGet]
        [TokenFilter]
        public JsonResult CargarTipoMuestreo(int? ID_Cuestionario)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            
            List<T_CboTipoMuestreo> lst = new List<T_CboTipoMuestreo>();
            lst = DB.ListarCboTipoMuestreo(ID_Cuestionario);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [TokenFilter]
        public ActionResult RegistroMuestreo(int? pCuestionario, string pTipoMuestreo)
        {
            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            List<T_Muestreo> lst = new List<T_Muestreo>();

            lst = DB.ListMuestreo(pCuestionario, pTipoMuestreo);

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == pCuestionario).ToList();

            if (lstCuestionario.Count > 0)
            {
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Publico = 0;
            }
            
            ViewBag.C_TipoMuestreo = pTipoMuestreo;

            return PartialView(lst);
        }

        [HttpPost]
        [TokenFilter]
        public JsonResult GrabarMuestreo(int ID_Muestreo, int N_Muestreo)
        {
            DB.GrabarCantMuestra(ID_Muestreo, N_Muestreo);
            return Json(ID_Muestreo, JsonRequestBehavior.AllowGet);
        }
        [TokenFilter]
        public ActionResult ReenvioInvitacion(int? N_Cuestionario)
        {
            if (!N_Cuestionario.HasValue) { N_Cuestionario = 0; }

            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            List<T_Area> lstArea = new List<T_Area>();
            List<T_Publico> lstPublico = new List<T_Publico>();
            List<T_CboEstado> lstEstado = new List<T_CboEstado>();
            lstArea = DB.T_Area.Where(item => item.N_Estado == true).ToList();
            lstPublico = DB.T_Publico.Where(item => item.N_Estado == true).ToList();
            lstEstado = DB.ListarCboEstado("T_Encuesta", 0);
            ViewBag.area = lstArea;
            ViewBag.publico = lstPublico;
            ViewBag.estado = lstEstado;

            List<T_Cuestionario> lstCuestionario = new List<T_Cuestionario>();
            lstCuestionario = DB.T_Cuestionario.Where(item => item.ID_Cuestionario == N_Cuestionario).ToList();

            if (lstCuestionario.Count == 0)
            {
                ViewBag.N_Area = lstArea.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstPublico.ElementAt(0).ID_Publico;
            }
            else
            {
                ViewBag.N_Area = lstCuestionario.ElementAt(0).ID_Area;
                ViewBag.N_Publico = lstCuestionario.ElementAt(0).ID_Publico;
            }

            ViewBag.N_Cuestionario = N_Cuestionario.Value;

            return View();
        }

        [HttpGet]
        [TokenFilter]
        public JsonResult CargarMuestreo(int? ID_Cuestionario, string ID_TipoMuestreo)
        {
            if (!ID_Cuestionario.HasValue) { ID_Cuestionario = 0; }
            List<T_CboEstrato> lst = new List<T_CboEstrato>();
            lst = DB.ListCboMuestreo(ID_Cuestionario, ID_TipoMuestreo);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [TokenFilter]
        public ActionResult ReenvioInvitacion_datos(int? pCuestionario, string pTipo, string pUbigeo, int? pEstadoE)
        {
            string cUsuario = "";
            if (TempData.ContainsKey("Usuario"))
            {
                cUsuario = Convert.ToString(TempData.Peek("Usuario"));
            }
            ViewBag.C_Usuario = cUsuario;

            if (!pCuestionario.HasValue) { pCuestionario = 0; }
            if (!pEstadoE.HasValue) { pEstadoE = 0; }
            if (pTipo=="") { pTipo = "All"; }
            if (pUbigeo == "") { pUbigeo = "All"; }
            List<T_Directorio> lst = new List<T_Directorio>();

            lst = DB.ListDirectorio(pCuestionario.Value, 2, pTipo, pUbigeo, pEstadoE.Value);
            ViewBag.N_Cuestionario = pCuestionario.Value;

            return PartialView(lst);
        }
    }
}