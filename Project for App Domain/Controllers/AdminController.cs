using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_for_App_Domain.Models;
using Project_for_App_Domain.Helpers;

namespace Project_for_App_Domain.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult GetAccountById(int id)
        {
            var dbContext = new SWE4713Entities();
            var account = dbContext.Users.Where(x => x.UserId == id); ;

            if (account != null)
            {
                AdminViewModel model = new AdminViewModel();

                foreach (var item in account)
                {
                    model.UserName = item.UserName;
                    model.UserType = item.UserType;
                    model.FirstName = item.FirstName;
                    model.LastName = item.LastName;
                    model.Email = item.Email;
                    model.DOB = Convert.ToDateTime(item.DOB);
                    model.Street = item.Street;
                    model.City = item.City;
                    model.State = item.State;
                    model.Zip = item.Zip;
                    model.Active = item.Active;
                    model.Attempts = Convert.ToInt32(item.Attempts);
                    model.DateCreated = item.DateCreated;
                    model.DateUpdated = Convert.ToDateTime(item.DateUpdated);
                }

                return PartialView("_GridEditPartial", model);
            }

            return View();
        }

        // GET: Admin
        public ActionResult Index(int? pageNumber)
        {
            var dbContext = new SWE4713Entities();
            AdminViewModel model = new AdminViewModel();
            model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            model.PageSize = 4;

            List<User> accounts = dbContext.Users.ToList();

            if (accounts != null)
            {
                model.AccountList = accounts.OrderBy(x => x.UserId)
                          .Skip(model.PageSize * (model.PageNumber - 1))
                          .Take(model.PageSize).ToList();

                model.TotalCount = accounts.Count();
                var page = (model.TotalCount / model.PageSize) -
                           (model.TotalCount % model.PageSize == 0 ? 1 : 0);
                model.PagerCount = page + 1;
            }

            return View(model);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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

        public ActionResult WebGrid()
        {
            IndexViewModel model = new IndexViewModel();
            return View();
            
        }
    }
}
