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
    public class LienContactGridController : Controller
    {
        public ActionResult getLienContacts(JqGridRequest request, string selectedLienId)
        {
            var context = new EFRepository();

            var lienid = Convert.ToInt32(selectedLienId);

            var contactList =
                from C in context.EFLienContacts
                where C.LienId == lienid
                select C;

            JqGridResponse jqGridResp = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)contactList.Count() / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = contactList.Count()
            };

            foreach (EFLienContact contact in contactList)
            {
                jqGridResp.Records.Add(new JqGridRecord(Convert.ToString(contact.Id), new List<object>() {
                    contact.Id.ToString(),
                    contact.LienId.ToString(), // change to propertyId
                    contact.FirstName,
                    contact.LastName,
                    contact.Role
                    //Contact.PhoneHome,
                    //Contact.PhoneWork,
                    //Contact.PhoneCell,
                    //Contact.PhoneFax,
                    //Contact.Email
                }));
            }
            return new JqGridJsonResult() { Data = jqGridResp };
        }

        public ActionResult editContact(string oper, LienContactGridModel editContact)
        {
            var context = new EFRepository();

            if (oper.Equals("edit"))
            {
                EFLienContact contact = new EFLienContact();
                contact.Id = Convert.ToInt32(editContact.Id);
                contact.LienId = Convert.ToInt32(editContact.LienId);
                contact.FirstName = editContact.FirstName;
                contact.LastName = editContact.LastName;
                contact.Role = editContact.Role;
                context.Entry(contact).State = EntityState.Modified;
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult addContact(string oper, LienContactGridModel addContact, string selectedLienId)
        {
            var context = new EFRepository();

            if (oper.Equals("add"))
            {
                EFLienContact contact = new EFLienContact();
                contact.Id = 0;
                contact.LienId = Convert.ToInt32(selectedLienId);
                contact.FirstName = addContact.FirstName;
                contact.LastName = addContact.LastName;
                contact.Role = addContact.Role;
                context.EFLienContacts.Add(contact);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }

        public ActionResult deleteContact(string oper, string id)
        {
            var context = new EFRepository();

            if (oper.Equals("del"))
            {
                EFLienContact deleteContact = new EFLienContact() { Id = Convert.ToInt32(id) };
                context.EFLienContacts.Attach(deleteContact);
                context.EFLienContacts.Remove(deleteContact);
                context.SaveChanges();
            }
            return Json(JSONResponseFactory.SuccessResponse());
        }
    }
}