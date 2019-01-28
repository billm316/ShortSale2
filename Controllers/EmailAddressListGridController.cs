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
    public class EmailAddressListGridController : Controller
    {
        public ActionResult getEmailAddressList(JqGridRequest request, string propertyId)
        {
            var context = new EFRepository();
            int propid = Convert.ToInt32(propertyId);
            var property = from Property 
                               in context.EFProperties 
                               where Property.Id == propid
                           select Property;

            var ContactList =
                from Contact
                    in context.EFContacts
                where Contact.PropertyId == propid && 
                Contact.Email != null && 
                Contact.Role == "P.Seller" ||
                Contact.Role == "Seller" || 
                Contact.Role == "L.Agent"
                select Contact;

            int totalRecords = ContactList.Count();
            JqGridResponse jqGridResp = new JqGridResponse();

            foreach (EFContact Contact in ContactList)
            {
                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(Contact.Id), new List<object>() {
                    Contact.Email,
                    Contact.FirstName + " " + Contact.LastName,
                    "property address",
                    ""
                }));
            }

            jqGridResp.TotalPagesCount = totalRecords;
            jqGridResp.PageIndex = request.PageIndex;
            jqGridResp.TotalRecordsCount = totalRecords;

            return new JqGridJsonResult() { Data = jqGridResp };
        }
        
	}
}