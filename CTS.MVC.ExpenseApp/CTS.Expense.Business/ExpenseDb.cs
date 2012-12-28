using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using CTS.Expense.Domain;

namespace CTS.Expense.Business
{
    public class ExpenseDb : DbContext
    {
        // this is what enables the creation of the Table on the DB 
        public DbSet<Report> Reports { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Establishment> EstablishmentTBL { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetail { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
