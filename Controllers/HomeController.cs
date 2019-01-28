using System;

using System.Security.Claims;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Lib.Web.Mvc.JQuery.JqGrid;
using ShortSale2.Models;
using ShortSale2.App_Data;

namespace ShortSale2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;
            return View("PropertySummary");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult PropertySummary()
        {
            //ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;
            return View();
        }

        public ActionResult PropertyDetail(string vbPropertyId)
        {
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var property = context.EFProperties.Where(a => a.Id == propid).Single();

            ViewData["vbPropertyId"] = vbPropertyId;
            ViewData["Address"] = property.Address + "  " + property.City + ", " + property.State + " " + property.Zip;
            return View();
        }

        public ActionResult LienNegotiation(string vbPropertyId)
        {
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var property = context.EFProperties.Where(a => a.Id == propid).Single();
            var Seller = context.EFContacts.Where(c => c.PropertyId == propid && c.Role == "P.Seller").FirstOrDefault();

            ViewData["vbPropertyId"] = vbPropertyId;
            ViewData["Address"] = property.Address + "  " + property.City + ", " + property.State + " " + property.Zip;
            if (Seller != null)
            {
                ViewData["Seller"] = Seller.FirstName + " " + Seller.LastName + " ";
                ViewData["Last6"] = "xxx-12-3456";
            }
            else
            {
                ViewData["Seller"] = "";
                ViewData["Last6"] = "xxx-12-3456";
            }

            return View();
        }

        public ActionResult Email(string vbPropertyId)
        {
            ViewData["PropertyId"] = vbPropertyId;

            return View();
        }
    }
}