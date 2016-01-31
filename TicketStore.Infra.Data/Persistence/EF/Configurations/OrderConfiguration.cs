﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TicketStore.Domain.Orders;

namespace TicketStore.Infra.Data.EF.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            base.HasKey(u => u.OrderId);

            base.HasRequired(p => p.Customer)
                .WithMany()
                .Map(m => m.MapKey("UserId"));

            base.HasRequired(p => p.Event)
                .WithMany()
                .Map(m => m.MapKey("EventId"));
        }
    }
}