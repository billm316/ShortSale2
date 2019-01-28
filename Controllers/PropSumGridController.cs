using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Lib.Web.Mvc.JQuery.JqGrid;
using ShortSale2.Models;
using ShortSale2.App_Data;

namespace ShortSale2.Controllers
{
    public class PropSumGridController : Controller
    {
        public ActionResult getProperties(JqGridRequest request)
        {
            var context = new EFRepository();
            var propertyList = from property 
                                   in context.EFProperties
                               where property.isDeleted == false
                               select property;

            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)propertyList.Count() / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = propertyList.Count()
            };

            foreach (EFProperty p in propertyList)
            {
                PropSumGridModel psModel = new PropSumGridModel();
                psModel.Id = p.Id.ToString();
                psModel.Type = p.Type;
                psModel.Address = p.Address;
                psModel.City = p.City;
                psModel.State = p.State;
                psModel.Zip = p.Zip;

                var lienList = from lien in context.EFLiens
                               where lien.PropertyId == p.Id && lien.LienPosition == "1"
                               select lien;
                foreach (EFLien q in lienList)
                {
                    psModel.Servicer = q.Servicer;
                    psModel.Status = q.Status;
                }

                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(p.Id), new List<object>() {
                        psModel.Id,
                        psModel.Type,
                        psModel.Address,
                        psModel.City,
                        psModel.State,
                        psModel.Zip,
                        psModel.Servicer,
                        psModel.Status
                    }));
            }            
            return new JqGridJsonResult() { Data = jqGridResp };
        }

        public ActionResult editProperty(string oper, PropSumGridModel editProperty)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                EFProperty p = new EFProperty();
                p.Id = Convert.ToInt32(editProperty.Id);
                p.Type = editProperty.Type;
                p.Address = editProperty.Address;
                p.City = editProperty.City;
                p.State = editProperty.State;
                p.Zip = editProperty.Zip;
                context.Entry(p).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addProperty(string oper, EFProperty addProperty)
        {
            var context = new EFRepository();

            if (oper.Equals("add"))
            {
                EFProperty p = new EFProperty();
                p.Id = 0;
                p.Type = addProperty.Type;
                p.Address = addProperty.Address;
                p.City = addProperty.City;
                p.State = addProperty.State;
                p.Zip = addProperty.Zip;
                context.EFProperties.Add(p);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult deleteProperty(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                int propId = Convert.ToInt32(id);
                var property = (from p 
                                    in context.EFProperties
                                where p.Id == propId
                                select p).Single();

                property.isDeleted = true;
                context.Entry(property).State = EntityState.Modified;
                context.SaveChanges();

                //EFProperty deleteProperty = new EFProperty() { Id = Convert.ToInt32(id) };
                //context.EFProperties.Attach(deleteProperty);
                //context.EFProperties.Remove(deleteProperty);
                //context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}