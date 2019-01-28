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
    public class SellerSumGridController : Controller
    {
        public ActionResult getSellers(JqGridRequest request, string vbPropertyId)
        {
            var context = new EFRepository();
            var propid = Convert.ToInt32(vbPropertyId);
            var contactList =
                from contact
                    in context.EFContacts
                where contact.PropertyId == propid &&
                contact.Role == "P.Seller" ||
                contact.Role == "Seller" ||
                contact.Role == "L.Agent"
                select contact;

            int totalRecords = contactList.Count();
            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            foreach (EFContact Contact in contactList)
            {
                string autoStatusUpdate = "False";
                if (Contact.AutoStatusUpdate == true)
                    autoStatusUpdate = "True";

                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(Contact.Id), new List<object>() {
                    Contact.Id.ToString(),
                    Contact.PropertyId.ToString(), // change to propertyId
                    Contact.FirstName,
                    Contact.LastName,
                    Contact.Role,
                    //Contact.PhoneHome,
                    //Contact.PhoneWork,
                    //Contact.PhoneCell,
                    //Contact.PhoneFax,
                    Contact.Email,
                    autoStatusUpdate
                }));
            }
            return new JqGridJsonResult() { Data = jqGridResp };
        }

        public ActionResult editContact(string oper, SellerSumGridModel editContact)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                EFContact contact = new EFContact();
                contact.Id = Convert.ToInt32(editContact.Id);
                contact.PropertyId = Convert.ToInt32(editContact.PropertyId);
                contact.FirstName = editContact.FirstName;
                contact.LastName = editContact.LastName;
                contact.Role = editContact.Role;
                contact.Email = editContact.Email;
                if (editContact.AutoStatusUpdate == "True")
                    contact.AutoStatusUpdate = true;
                else
                    contact.AutoStatusUpdate = false;

                context.Entry(contact).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addContact(string oper, SellerSumGridModel addContact, string vbPropertyId)
        {
            var context = new EFRepository();

            if (oper.Equals("add"))
            {
                EFContact contact = new EFContact();
                contact.Id = 0;
                contact.PropertyId = Convert.ToInt32(vbPropertyId);
                contact.FirstName = addContact.FirstName;
                contact.LastName = addContact.LastName;
                contact.Role = addContact.Role;
                contact.Email = addContact.Email;
                if (addContact.AutoStatusUpdate == "True")
                    contact.AutoStatusUpdate = true;
                else
                    contact.AutoStatusUpdate = false;

                context.EFContacts.Add(contact);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult deleteContact(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                EFContact deleteContact = new EFContact() { Id = Convert.ToInt32(id) };
                context.EFContacts.Attach(deleteContact);
                context.EFContacts.Remove(deleteContact);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}
