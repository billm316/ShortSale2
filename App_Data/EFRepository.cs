using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShortSale2.Models;

namespace ShortSale2.App_Data
{
    public class EFRepository : DbContext
    {
        public EFRepository()
            : base("name=ShortSaleDBContext")
        {
        }
        public DbSet<EFProperty> EFProperties { get; set; }
        public DbSet<EFContact> EFContacts { get; set; }
        public DbSet<EFLien> EFLiens { get; set; }
        public DbSet<EFLienContact> EFLienContacts { get; set; }
        public DbSet<EFOffer> EFOffers { get; set; }
        public DbSet<EFDocDesc> EFDocDescs { get; set; }
        public DbSet<EFDocHist> EFDocHists { get; set; }
        public DbSet<EFDoc> EFDocDocs { get; set; }
    }
}
