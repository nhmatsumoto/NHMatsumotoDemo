using NHMatsumotoDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NHMatsumotoDemo.Infrastructure.Database.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .UseMySqlIdentityColumn<int>()
               .ValueGeneratedOnAdd();

            builder
               .HasOne<Endereco>(x => x.Endereco);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Senha)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);
        }
    }
}
