using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.EntityConfiguration
{
    public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {

            builder.ToTable("FinancialTransactions");

            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(ft => ft.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ft => ft.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(ft => ft.Description)
                .HasMaxLength(255) 
                .IsRequired(false); 

            builder.Property(ft => ft.Date)
                .HasColumnType("datetime")
                .IsRequired();


        }
    }
}
