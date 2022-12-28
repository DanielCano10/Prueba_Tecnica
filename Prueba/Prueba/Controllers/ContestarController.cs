using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba.Models;
using Prueba.Permisos;

namespace Prueba.Controllers
{
    public class ContestarController : Controller
    {
        private pruebadbEntities db = new pruebadbEntities();
        // GET: Contestar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visualizar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campo campo = db.Campo.Find(id);
            if (campo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEncuesta = new SelectList(db.Encuesta, "id_encuesta", "descripcion", campo.idEncuesta);
            return View(campo);
        }
    }
}