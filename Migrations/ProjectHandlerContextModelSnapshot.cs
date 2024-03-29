﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectHandler.Models;

namespace ProjectHandler.Migrations
{
    [DbContext(typeof(ProjectHandlerContext))]
    partial class ProjectHandlerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHandler.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ProjectCompletionDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Project_Completion_Date");

                    b.Property<DateTime>("ProjectCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Project_Created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ProjectName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Project_Name");

                    b.Property<int?>("ProjectPriority")
                        .HasColumnType("int")
                        .HasColumnName("Project_Priority");

                    b.Property<DateTime?>("ProjectStartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Project_Start_Date");

                    b.Property<string>("ProjectStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Project_Status");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProjectName" }, "UQ__Project__0BBE2138E46EEFAE")
                        .IsUnique()
                        .HasFilter("[Project_Name] IS NOT NULL");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectHandler.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("Project_ID");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Task_Description");

                    b.Property<string>("TaskName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Task_Name");

                    b.Property<int?>("TaskPriority")
                        .HasColumnType("int")
                        .HasColumnName("Task_Priority");

                    b.Property<string>("TaskStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Task_Status");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("ProjectHandler.Models.Task", b =>
                {
                    b.HasOne("ProjectHandler.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__Task__Project_ID__34C8D9D1")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectHandler.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
