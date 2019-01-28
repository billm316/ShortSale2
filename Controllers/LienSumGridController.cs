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
    public class LienSumGridController : Controller
    {
        public ActionResult getLiens(JqGridRequest request, string vbPropertyId)
        {
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var lienList =
                from lien in context.EFLiens
                where lien.PropertyId == propid
                orderby lien.LienPosition ascending
                select lien;

            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalRecordsCount = lienList.Count(),
                TotalPagesCount = (int)Math.Ceiling((float)lienList.Count() / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
            };

            foreach (EFLien lien in lienList)
            {
                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(lien.Id), new List<object>() {
                    lien.Id.ToString(),
                    lien.PropertyId.ToString(), 
                    //lien.ReviewDate.ToString(),
                    lien.ReviewDate.HasValue ? Convert.ToDateTime(lien.ReviewDate).Date.ToShortDateString() : "",
                    lien.Type,
                    lien.Servicer,
                    lien.AccountNo,
                    lien.LienPosition,
                    lien.SettlementAmt.ToString(),
                    lien.SettlementAmt == 0 ? "" : 
                        lien.SettlementDate.HasValue ? Convert.ToDateTime(lien.SettlementDate).Date.ToShortDateString() : "",
                    //lien.SettlementDate.Date.ToShortDateString()
                    lien.Status,
                    lien.MinNetProceeds.ToString(),
                    lien.MaxNetProceeds.ToString(),
                    lien.Valuation.ToString(),
                    lien.ValuationDate.HasValue ? Convert.ToDateTime(lien.ValuationDate).Date.ToShortDateString() : "",
                    lien.PayoffAmt.ToString(),
                    lien.PayoffDate.HasValue ? Convert.ToDateTime(lien.PayoffDate).Date.ToShortDateString() : "",
                    lien.SettlementProgram,
                    lien.FHA,
                    lien.Investor,
                    ""
                }));
            }
            return new JqGridJsonResult() { Data = jqGridResp };
        }

        public ActionResult editLien(string oper, LienSumGridModel editLien, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                EFLien lien = new EFLien();
                lien.Id = Convert.ToInt32(editLien.Id);
                lien.PropertyId = Convert.ToInt32(editLien.PropertyId);
                lien.ReviewDate = editLien.ReviewDate == null ? (DateTime?)null : Convert.ToDateTime(editLien.ReviewDate);
                lien.Type = editLien.Type;
                lien.Servicer = editLien.Servicer;
                lien.AccountNo = editLien.AccountNo;
                lien.LienPosition = editLien.LienPosition;
                lien.SettlementAmt = Convert.ToDecimal(editLien.SettlementAmt);
                lien.SettlementDate = editLien.SettlementDate == null ? (DateTime?)null : Convert.ToDateTime(editLien.SettlementDate);
                lien.Status = editLien.Status;
                lien.MinNetProceeds = Convert.ToDecimal(editLien.MinNetProceeds);
                lien.MaxNetProceeds = Convert.ToDecimal(editLien.MaxNetProceeds);
                lien.Valuation = Convert.ToDecimal(editLien.Valuation);
                lien.ValuationDate = editLien.ValuationDate == null ? (DateTime?)null : Convert.ToDateTime(editLien.ValuationDate);
                lien.PayoffAmt = Convert.ToDecimal(editLien.PayoffAmt);
                lien.PayoffDate = editLien.PayoffDate == null ? (DateTime?)null : Convert.ToDateTime(editLien.PayoffDate);
                lien.SettlementProgram = editLien.SettlementProgram;
                lien.FHA = editLien.FHA;
                lien.Investor = editLien.Investor;
                context.EFLiens.Attach(lien);
                context.Entry(lien).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addLien(string oper, LienSumGridModel addLien, string vbPropertyId)
        {
            var context = new EFRepository();

            if (oper.Equals("add"))
            {
                EFLien lien = new EFLien();
                lien.Id = 0;
                lien.PropertyId = Convert.ToInt32(vbPropertyId);
                lien.ReviewDate = addLien.ReviewDate == null ? (DateTime?)null : Convert.ToDateTime(addLien.ReviewDate);
                lien.Type = addLien.Type;
                lien.Servicer = addLien.Servicer;
                lien.AccountNo = addLien.AccountNo;
                lien.LienPosition = addLien.LienPosition;
                lien.SettlementAmt = Convert.ToDecimal(addLien.SettlementAmt);
                lien.SettlementDate = addLien.SettlementDate == null ? (DateTime?)null : Convert.ToDateTime(addLien.SettlementDate);
                lien.Status = addLien.Status;
                lien.MinNetProceeds = Convert.ToDecimal(addLien.MinNetProceeds);
                lien.MaxNetProceeds = Convert.ToDecimal(addLien.MaxNetProceeds);
                lien.Valuation = Convert.ToDecimal(addLien.Valuation);
                lien.ValuationDate = addLien.ValuationDate == null ? (DateTime?)null : Convert.ToDateTime(addLien.ValuationDate);
                lien.PayoffAmt = Convert.ToDecimal(addLien.PayoffAmt);
                lien.PayoffDate = addLien.PayoffDate == null ? (DateTime?)null : Convert.ToDateTime(addLien.PayoffDate);
                lien.SettlementProgram = addLien.SettlementProgram;
                lien.FHA = addLien.FHA;
                lien.Investor = addLien.Investor;

                context.EFLiens.Add(lien);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult deleteLien(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                EFLien deleteLien = new EFLien() { Id = Convert.ToInt32(id) };
                context.EFLiens.Attach(deleteLien);
                context.EFLiens.Remove(deleteLien);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}