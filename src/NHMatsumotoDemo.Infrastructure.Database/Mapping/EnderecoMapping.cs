using NHMatsumotoDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NHMatsumotoDemo.Infrastructure.Database.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .UseMySqlIdentityColumn<int>()
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Rua)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Complemento)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Logradouro)
               .IsRequired()
               .HasColumnType("varchar(50)")
               .HasMaxLength(50);

            builder.Property(x => x.Cidade)
               .IsRequired()
               .HasColumnType("varchar(255)")
               .HasMaxLength(255);

            builder.Property(x => x.Esatado)
              .IsRequired()
              .HasColumnType("varchar(255)")
              .HasMaxLength(255);

            builder.Property(x => x.CodigoPostal)
               .IsRequired()
               .HasColumnType("varchar(8)")
               .HasMaxLength(8);

            builder.Property(x => x.Pais)
               .IsRequired()
               .HasColumnType("varchar(255)")
               .HasMaxLength(255);
        }
    }
}
