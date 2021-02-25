using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Project_for_App_Domain.Models;

namespace Project_for_App_Domain.Controllers
{
    public class ChartAccountController : Controller
    {
        SWE4713Entities db = new SWE4713Entities();

        // GET: ChartAccount
        public ActionResult Index()
        {
            return View(db.ChartAccounts.ToList());
        }

        // GET: ChartAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChartAccount/Create
        public ActionResult Create()
        {
            // TODO: check for duplicate account number already in the table

            return View();
        }

        // POST: ChartAccount/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ChartAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChartAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ChartAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChartAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
