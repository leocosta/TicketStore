namespace TicketStore.Infra.Data.Persistence.EF.Migrations
{
    using System.Data.Entity.Migrations;
    using Domain.CreditCards;
    using System;
    using Contexts;
    using Domain.Events;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TicketStoreContext context)
        {
            //  This method will be called after migrating to the latest version. 
            context.Users.AddOrUpdate(u => u.Email, 
              new User
              {
                  UserId = 1,
                  Name = "Leonardo Costa",
                  Birthdate = new DateTime(1981, 04, 21),
                  Email = "leoccosta@outlook.com",
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  Gender = Gender.Male,
                  Password = "123456",
                  SSN = "05522078707",
                  Address = new Address()
                  {
                      Line1 = "Rua Min. Jo�o Alberto, 30",
                      Line2 = "Casa",
                      State = "RJ",
                      ZipCode = "25555130",
                      City = "S�o Jo�o de Meriti"
                  }
              }
            );

            context.Events.AddOrUpdate(e => e.EventId,
              new Event
              {
                  EventId = 1,
                  Name = "Aprenda a Investir na Bolsa",
                  StartDate = DateTime.Today.AddDays(30).AddHours(9),
                  EndDate = DateTime.Today.AddDays(35).AddHours(17),
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  CardImageUrl = @"http://ww2.baguete.com.br/admin//cache/sites/default/files/multimedia/imagens/noticia/92094-stock-market-rebounds-worst-day-year-read-more-http-business-time-com20130416stock-market-rebounds-w.jpg",
                  Description = @"Neste curso passamos 10 ou 12 horas mostrando o ferramental dos mercados de renda vari�vel, 
                                falamos sobre as bolsas de valores, sobre a��es, op��es, mercados futuros e fazemos uma introdu��o 
                                �s duas escolas de an�lises: t�cnica e fundamentalista.",
                  Location = new Address()
                  {
                      Line1 = "Av. das Am�ricas, 500 - Barra da Tijuca",
                      Line2 = "Bloco 21, Sala 248 - Shopping Downtown",
                      State = "RJ",
                      City = "Rio de Janeiro",
                      ZipCode = "22115456"
                  },
                  Price = 150
              },
              new Event
              {
                  EventId = 2,
                  Name = "Curso de Forma��o Completa em Pilates",
                  StartDate = DateTime.Today.AddDays(15).AddHours(9),
                  EndDate = DateTime.Today.AddDays(15).AddHours(17),
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  CardImageUrl = @"http://www.studiobalance.com.br/wp-content/uploads/2015/10/pilates-beneficios-para-o-corpo-esteira-02-oda-em-forma.jpg",
                  Description = @"Seja bem vindo � Pilates Institute! (21) 3403-3402. Aproveite seu curso!",
                  Location = new Address()
                  {
                      Line1 = "Hil�rio de Gouveia, 66 - Copacabana - Rio de Janeiro",
                      Line2 = "Sala 705 e 706 - Espa�o Vital Fit",
                      State = "RJ",
                      City = "Rio de Janeiro",
                      ZipCode = "22040020"
                  },
                  Price = 800
              }
            );
        }
    }
}
