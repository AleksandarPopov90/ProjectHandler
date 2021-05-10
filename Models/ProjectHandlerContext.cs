using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectHandler.Models
{
    public partial class ProjectHandlerContext : DbContext
    {
        public ProjectHandlerContext()
        {
        }

        public ProjectHandlerContext(DbContextOptions<ProjectHandlerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("ProjectHandler");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.ProjectName, "UQ__Project__0BBE2138E46EEFAE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Project_Completion_Date");

                entity.Property(e => e.ProjectCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Project_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Project_Name");

                entity.Property(e => e.ProjectPriority).HasColumnName("Project_Priority");

                entity.Property(e => e.ProjectStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Project_Start_Date");

                entity.Property(e => e.ProjectStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Project_Status");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.Property(e => e.TaskDescription)
                    .HasMaxLength(500)
                    .HasColumnName("Task_Description");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Task_Name");

                entity.Property(e => e.TaskPriority).HasColumnName("Task_Priority");

                entity.Property(e => e.TaskStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Task_Status");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Task__Project_ID__34C8D9D1")
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
