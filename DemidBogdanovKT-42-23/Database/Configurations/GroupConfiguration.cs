using DemidBogdanovKT_42_23.Database.Helpers;
using DemidBogdanovKT_42_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemidBogdanovKT_42_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(p => p.GroupId).HasName($"pk_{TableName}_group_id");

            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd()
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название группы");

            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("c_group_course")
                .HasColumnType(ColumnType.Int)
                .HasComment("Курс");

            builder.Property(p => p.SpecialtyId)
                .IsRequired()
                .HasColumnName("specialty_id")
                .HasComment("Идентификатор специальности");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("c_group_isdeleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления");

            builder.ToTable(TableName)
                .HasOne(p => p.Specialty)
                .WithMany()
                .HasForeignKey(p => p.SpecialtyId)
                .HasConstraintName("fk_f_specialty_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.SpecialtyId, $"idx_{TableName}_fk_f_specialty_id");

            builder.Navigation(p => p.Specialty).AutoInclude();
        }
    }
}