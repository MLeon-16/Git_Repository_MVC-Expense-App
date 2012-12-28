namespace CTS.Expense.Business.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CTS.Expense.Domain;
    /// <summary>
    /// this class was first set to internal needed to set to public so Gloabal.asax.cs would have access to it 
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<CTS.Expense.Business.ExpenseDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; /// this will allow it to override the DB when needed 

        }

        protected override void Seed(CTS.Expense.Business.ExpenseDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Project.AddOrUpdate(
            //    new Project { Id = 111, ProjectName="MVC PRJ", ClientId = 111 },
            //    new Project { Id = 222, ProjectName = "MVC PRJ", ClientId = 222 }
                
            //    );

            //context.Clients.AddOrUpdate(
            //    new Client { Id = 111 , ClientName = "Miguel Leon"  },
            //    new Client { Id = 222, ClientName = "Miguel Leon" }



            //    );

            //context.Reports.AddOrUpdate(
            //    new Report { Id =111, Title="Test Report", Billable = true, ErNumber= 100 , MonthYear = new DateTime(2010,1,18) , ProjectID  = true},
            //     new Report { Id = 222, Title = "YO", Billable = true, ErNumber = 200, MonthYear = new DateTime(2012, 1, 18), ProjectID = true }
                
            //    );

            //context.SaveChanges();

        }
    }
}
