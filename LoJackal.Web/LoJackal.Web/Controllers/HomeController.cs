using LoJackal.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoJackal.Web.Views.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString)
        {
            var list = from c in new Models.LoJackal().Communications
                       group c by c.ComputerName into computers
                       select computers.OrderByDescending(comp => comp.LastCommunicated).FirstOrDefault();

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(l => l.ComputerName.Contains(searchString));
            }

            return View(list);
        }

        public ActionResult All()
        {
            var list = new Models.LoJackal().Communications.OrderByDescending(c => c.LastCommunicated).ToList();

            return View(list);
        }

        public ActionResult Configurations()
        {
            var list = new Models.LoJackal().Configurations.ToList();

            return View(list);
        }

        [HttpGet]
        public ActionResult EditConfiguration(int id = 0)
        {
            using (var db = new Models.LoJackal())
            {
                var config = db.Configurations.Find(id);
                if (config == null)
                {
                    return HttpNotFound();
                }
                return View(config);

            }
        }

        [HttpPost]
        public ActionResult EditConfiguration(Models.Configuration config)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Models.LoJackal())
                {
                    db.Entry(config).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Configurations");
                }
            }
            return View(config);
        }
    }
}