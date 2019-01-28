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
    public class DocDescGridController : Controller
    {
        public ActionResult getDocDescs(JqGridRequest request, string vbPropertyId)
        {
            var context = new EFRepository();

            var docDescList = from docDesc
                                  in context.EFDocDescs
                              select docDesc;

            int totalRecords = docDescList.Count();
            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            foreach (EFDocDesc dd in docDescList)
            {
                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(dd.Id), new List<object>() {
                    dd.Id.ToString(),
                    vbPropertyId,
                    dd.Type,
                    dd.Desc,
                    dd.ExpireDate.Date.ToShortDateString(),
                    dd.CreateDate.Date.ToShortDateString(),
                    dd.FileName,
                    dd.DocId == 0 ? "Upload File" : ""
                }));
            }
            return new JqGridJsonResult() { Data = jqGridResp };
        }

        // id is rowid and primary key of DocDesc record
        public ActionResult editDocDesc(string oper, EFDocDesc editDocDesc, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                editDocDesc.Id = Convert.ToInt32(id);
                context.EFDocDescs.Attach(editDocDesc);
                context.Entry(editDocDesc).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addDocDesc(string oper, DocDescGridModel DocDesc, string vbPropertyId)
        {
            var context = new EFRepository();

            //int propId = Convert.ToInt32(ViewData["vbPropertyId"]);
            int propId = Convert.ToInt32(vbPropertyId);

            if (oper.Equals("add"))
            {
                EFDocDesc doc = new EFDocDesc();
                doc.Id = 0;
                doc.PropertyId = Convert.ToInt32(vbPropertyId);
                doc.Type = DocDesc.Type;
                doc.Desc = DocDesc.Desc;
                doc.ExpireDate = Convert.ToDateTime(DocDesc.ExpireDate);
                doc.CreateDate = Convert.ToDateTime(DocDesc.CreateDate);
                doc.FileName = DocDesc.FileName;
                context.EFDocDescs.Add(doc);
                context.SaveChanges();

                EFDocHist hist = new EFDocHist();
                hist.EFDocDescId = doc.Id;
                hist.Date = DateTime.Now;
                hist.Action = "request";
                context.EFDocHists.Add(hist);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        // id is rowid and primary key of DocDesc record
        public ActionResult deleteDocDesc(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                EFDocDesc deleteDocDesc = new EFDocDesc() { Id = Convert.ToInt32(id) };
                context.EFDocDescs.Attach(deleteDocDesc);
                context.EFDocDescs.Remove(deleteDocDesc);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult genericDocDescList(string vbPropertyId)
        {
            var context = new EFRepository();

            //int propId = Convert.ToInt32(ViewData["vbPropertyId"]);
            int propId = Convert.ToInt32(vbPropertyId);

            
            DateTime now = DateTime.Now;
            for (int x = 0; x < 4; x++)
            {
                EFDocDesc doc = new EFDocDesc();
                doc.Id = 0;
                doc.PropertyId = Convert.ToInt32(vbPropertyId);
                doc.Type = "Paystub";
                doc.Desc = "Most Recent";
                doc.ExpireDate = now;
                doc.CreateDate = now;
                doc.FileName = "";
                context.EFDocDescs.Add(doc);
                context.SaveChanges();

                EFDocHist hist = new EFDocHist();
                hist.EFDocDescId = doc.Id;
                hist.Date = now;
                hist.Action = "Request";
                context.EFDocHists.Add(hist);
                context.SaveChanges();
            }

            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}