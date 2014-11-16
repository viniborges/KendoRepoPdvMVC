using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PDV.DAL;
using PDV.Models;


namespace PDV.Controllers
{
    public class ProdutonovoController : BaseController
    {

        private Contexto db = new Contexto();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Produtos_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetProdutos().ToDataSourceResult(request));
        }

        private static IEnumerable<Produto> GetProdutos()
        {
            Contexto db = new Contexto();
            return db.Produtos.ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Produto product)
        {
            if (product != null && ModelState.IsValid)
            {
                db.Produtos.Add(product);
                db.SaveChangesAsync();
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Produto product)
        {
            //if (product.ProdutoID == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Produto produto = db.Produtos.Find(product.ProdutoID);
            if (produto != null)
            {
                db.Produtos.Remove(produto);
                db.SaveChanges();

            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Produto product)
        {
            if (product != null && ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChangesAsync();

            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }


	}
}