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
    public class CampoesController : Controller
    {
        private pruebadbEntities db = new pruebadbEntities();
        [ValidateSession]
        // GET: Campoes
        public ActionResult Index()
        {
            var campo = db.Campo.Include(c => c.Encuesta);
            return View(campo.ToList());
        }

        // GET: Campoes/Details/5
        public ActionResult Details(int? id)
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
            return View(campo);
        }

        // GET: Campoes/Create
        public ActionResult Create()
        {
            ViewBag.idEncuesta = new SelectList(db.Encuesta, "id_encuesta", "descripcion");
            return View();
        }

        // POST: Campoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_campo,idEncuesta,nombre,titulo,requerido,tipo")] Campo campo)
        {
            if (ModelState.IsValid)
            {
                db.Campo.Add(campo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEncuesta = new SelectList(db.Encuesta, "id_encuesta", "descripcion", campo.idEncuesta);
            return View(campo);
        }

        // GET: Campoes/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Campoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_campo,idEncuesta,nombre,titulo,requerido,tipo")] Campo campo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEncuesta = new SelectList(db.Encuesta, "id_encuesta", "descripcion", campo.idEncuesta);
            return View(campo);
        }

        // GET: Campoes/Delete/5
        public ActionResult Delete(int? id)
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
            return View(campo);
        }

        // POST: Campoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campo campo = db.Campo.Find(id);
            db.Campo.Remove(campo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RedirigirCampos()
        {
            return RedirectToAction("Index", "Compoes");
        }
    }
}
