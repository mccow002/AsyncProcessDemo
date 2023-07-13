using AsyncDemo.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsyncDemo.Data.Config;

public class OrderLineItemRouteConfig : IEntityTypeConfiguration<OrderLineItemRoute>
{
    public void Configure(EntityTypeBuilder<OrderLineItemRoute> builder)
    {
        builder.Property(x => x.OrderLineItemRouteId)
            .ValueGeneratedOnAdd();
    }
}