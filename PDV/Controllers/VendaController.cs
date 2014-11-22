using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PDV.Models;
using PDV.DAL;

namespace PDV.Controllers
{
    public class VendaController : Controller
    {
        private Contexto db = new Contexto();

        // GET: /Venda/
        public ActionResult Index()
        {
            var vendas = db.Vendas.Include(v => v.Usuario);
            return View(vendas.ToList());
        }

        // GET: /Venda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: /Venda/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Email");
            return View();
        }

        // POST: /Venda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(Venda venda)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    //Caso o VendaId seja maior que 0, significa que é uma edição de registro já existente
                    if (venda.VendaId > 0)
                    {
                        var VendaAtual = db.VendaItens.Where(p => p.VendaId == venda.VendaId);
                        foreach (VendaItem vi in VendaAtual)
                            db.VendaItens.Remove(vi);

                        foreach (VendaItem vi in venda.VendaItem)
                            db.VendaItens.Add(vi);

                        db.Entry(venda).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Vendas.Add(venda);
                    }
                    db.SaveChanges();
                    return Json(new {Success = 1, VendaID = venda.VendaId, ex = ""});
                //}
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }
            return Json(new {Success = 0, ex = new Exception("Ops, ocorreu um erro ao salvar").Message.ToString()});

        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="VendaId,ClienteId,DataMovimento,UsuarioId,ValorTotalVenda")] Venda venda)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Vendas.Add(venda);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Email", venda.UsuarioId);
        //    return View(venda);
        //}

        // GET: /Venda/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar";
            Venda venda = db.Vendas.Find(id);

            //Call Create View
            return View("Create", venda);
        }
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Venda venda = db.Vendas.Find(id);
        //    if (venda == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Email", venda.UsuarioId);
        //    return View(venda);
        //}

        // POST: /Venda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="VendaId,ClienteId,DataMovimento,UsuarioId,ValorTotalVenda")] Venda venda)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(venda).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Email", venda.UsuarioId);
        //    return View(venda);
        //}

        // GET: /Venda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: /Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Vendas.Find(id);
            db.Vendas.Remove(venda);
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
    }
}
