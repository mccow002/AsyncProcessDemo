using AsyncDemo.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsyncDemo.Data.Config;

public class OrderLineItemConfig : IEntityTypeConfiguration<OrderLineItem>
{
    public void Configure(EntityTypeBuilder<OrderLineItem> builder)
    {
        builder.Property(x => x.OrderLineItemId)
            .ValueGeneratedOnAdd();
    }
}