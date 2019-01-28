using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid;
using ShortSale2.Models;
using ShortSale2.App_Data;

namespace ShortSale2.Controllers
{
    public class DocHistGridController : Controller
    {
        public ActionResult getDocHist(JqGridRequest request, string docDescId)
        {
            var context = new EFRepository();
            int docdescid = Convert.ToInt32(docDescId);
            var DocHistList = from DocHist in context.EFDocHists
                              where DocHist.EFDocDescId == docdescid
                              select DocHist;

            int totalRecords = DocHistList.Count();
            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            foreach (EFDocHist dh in DocHistList)
            {
                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(dh.Id), new List<object>() {
                    " - ", // Buffer
                    dh.Id.ToString(),
                    dh.EFDocDescId.ToString(),
                    dh.EFContactId.ToString(),
                    dh.EFLienId.ToString(),
                    dh.Date.ToString(),
                    dh.Action
                }));
            }
            return new JqGridJsonResult() { Data = jqGridResp };
        }
    }
}