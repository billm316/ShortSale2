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
    public class OfferSumGridController : Controller
    {
        public ActionResult getOffers(JqGridRequest request, string vbPropertyId)
        {
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var offerList = from offer
                                in context.EFOffers
                            where offer.PropertyId == propid
                            select offer;

            int totalRecords = offerList.Count();
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            foreach (EFOffer p in offerList)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(p.Id), new List<object>() {
                    p.Id.ToString(),
                    vbPropertyId,
                    p.OfferDate.Date.ToShortDateString(),
                    p.OfferAmt.ToString(),
                    p.ExpireDate.Date.ToShortDateString(),
                    p.Status
                }));
            }
            return new JqGridJsonResult() { Data = response };
        }

        // id is rowid and primary key of Offer record
        public ActionResult editOffer(string oper, OfferSumGridModel editOffer, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                EFOffer offer = new EFOffer();
                offer.Id = Convert.ToInt32(editOffer.Id);
                offer.PropertyId = Convert.ToInt32(editOffer.PropertyId);
                offer.OfferDate = Convert.ToDateTime(editOffer.OfferDate);
                offer.OfferAmt = Convert.ToDecimal(editOffer.OfferAmt);
                offer.ExpireDate = Convert.ToDateTime(editOffer.ExpireDate);
                offer.Status = editOffer.Status;
                context.EFOffers.Attach(offer);
                context.Entry(offer).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addOffer(string oper, OfferSumGridModel addOffer, string vbPropertyId)
        {
            var context = new EFRepository();

            if (oper.Equals("add"))
            {
                EFOffer offer = new EFOffer();
                offer.Id = 0;
                offer.PropertyId = Convert.ToInt32(vbPropertyId);
                offer.OfferDate = Convert.ToDateTime(addOffer.OfferDate);
                offer.OfferAmt = Convert.ToDecimal(addOffer.OfferAmt);
                offer.ExpireDate = Convert.ToDateTime(addOffer.ExpireDate);
                offer.Status = addOffer.Status;
                context.EFOffers.Add(offer);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        // id is rowid and primary key of Offer record
        public ActionResult deleteOffer(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                EFOffer deleteOffer = new EFOffer() { Id = Convert.ToInt32(id) };
                context.EFOffers.Attach(deleteOffer);
                context.EFOffers.Remove(deleteOffer);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}