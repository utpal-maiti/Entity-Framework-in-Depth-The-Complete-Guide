using DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace FluentAPI.EntityConfiguration
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            ToTable("tbl_Course");

            HasKey(c => c.Id);
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);


            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);


            HasRequired(a => a.Author)
                .WithMany(a => a.Courses)
                .HasForeignKey(a => a.AuthorId)
                .WillCascadeOnDelete(false);


            HasMany(c => c.Tags)
                .WithMany(c => c.Courses)
                .Map(m =>
                {
                    m.ToTable("CourseTags");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("TagId");
                });

        HasRequired(c => c.Cover)
                .WithRequiredPrincipal(c => c.Course);

        }

    }
}
