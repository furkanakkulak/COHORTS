using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MovieStoreAppWebAPI.Entities;

using System.Reflection.Emit;

namespace MovieStoreAppWebAPI.Operations.DatabaseOperation.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
