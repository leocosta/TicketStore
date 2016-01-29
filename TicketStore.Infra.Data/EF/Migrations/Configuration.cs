namespace TicketStore.Infra.Data.EF.Migrations
{
    using System.Data.Entity.Migrations;
    using Domain.Users;
    using System;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.TicketStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexts.TicketStoreContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(
              new User
              {
                  UserId = 1,
                  Name = "Leonardo Costa",
                  Birthdate = new DateTime(1981, 04, 21),
                  Email = "leoccosta@outlook.com",
                  CreateDate = new DateTime(),
                  ModifyDate = new DateTime(),
                  Gender = Gender.Male,
                  Password = "123456",
                  SSN = "05522078707",
                  Address = new Address()
                  {
                      Line1 = "Rua Min. João Alberto, 30",
                      Line2 = "Casa",
                      State = "RJ",
                      ZipCode = "25555130",
                      City = "São João de Meriti"
                  }
              }
            );

        }
    }
}
