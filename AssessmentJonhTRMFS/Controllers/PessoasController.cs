using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentJonhTRMFS.Data;
using AssessmentJonhTRMFS.Models;

namespace AssessmentJonhTRMFS.Controllers
{
    public class PessoasController : Controller
    {
        PessoaRepository obj = new PessoaRepository();
        private AssessmentJonhTRMFSContext db = new AssessmentJonhTRMFSContext();

        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var pessoas = from s in db.Pessoas
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pessoas = pessoas.Where(s => s.Nome.Contains(searchString)
                                       || s.Sobrenome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pessoas = pessoas.OrderByDescending(s => s.Nome);
                    break;
                case "Date":
                    pessoas = pessoas.OrderBy(s => s.Nascimento);
                    break;
                case "date_desc":
                    pessoas = pessoas.OrderByDescending(s => s.Nascimento);
                    break;
                default:
                    pessoas = pessoas.OrderBy(s => s.Nome);
                    break;
            }
            return View(pessoas.ToList());


            //return View(db.Pessoas.Where(x=>x.Nome.Contains(searchString) || x.Sobrenome.Contains(searchString) || searchString == null).ToList());
        }
        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = obj.Buscar(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NomeId,Nome,Sobrenome,Nascimento")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                obj.Create(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = obj.Buscar(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NomeId,Nome,Sobrenome,Nascimento")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                obj.Edit(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = obj.Buscar(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            obj.Delete(obj.Buscar(id));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                obj.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
