using System;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Web;
using System.Data.Entity;
using Lib.Web.Mvc.JQuery.JqGrid;
using ShortSale2.Models;
using ShortSale2.App_Data;
using System.Linq;

namespace ShortSale2.Controllers
{
    public class FileUploadController : Controller
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FileUpload(string vbPropertyId, string docDescId)
        {
            // To get a reference to HttpContext.Current you need to replace 
            // HttpContext.Current with 
            // System.Web.HttpContext.Current
            // This is because Controller class defines a property named HttpContext that is defined as 
            // public HttpContextBase HttpContext { get; } HttpContext on Controller class returns 
            // HttpContextBase which does not have a Current property. Hence you need to properly resolve 
            // the namespace here.            
            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
            HttpPostedFile file = httpContext.Request.Files[0];
            //HttpPostedFile file = HttpContext.Request.Files[0];

            //HttpFileCollection files = HttpContext.Request.Files;

            // Read the uploaded file
            string fileName = file.FileName;
            byte[] binaryWriteArray = new
            byte[file.InputStream.Length];
            file.InputStream.Read(binaryWriteArray, 0, (int)file.InputStream.Length);

            FileInfo file_Info = new FileInfo(file.FileName);
            //string ext = file_Info.Extension;
            //string file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
            
            FileStream objfilestream = new FileStream(
                Server.MapPath("/App_Data/UploadedFiles/" + fileName), 
                FileMode.Create, 
                FileAccess.ReadWrite);

            objfilestream.Write(binaryWriteArray, 0, binaryWriteArray.Length);
            objfilestream.Close();

            // Update docDescDesc
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var docdescid = Convert.ToInt32(docDescId);

            var docDesc = (from docdesc
                                  in context.EFDocDescs
                                  where docdesc.Id == docdescid &&
                                  docdesc.PropertyId == propid
                              select docdesc).Single();
            docDesc.DocId = 1;
            context.EFDocDescs.Attach(docDesc);
            context.Entry(docDesc).State = EntityState.Modified;
            context.SaveChanges();
        }   
    }
}