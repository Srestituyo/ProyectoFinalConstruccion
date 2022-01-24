﻿// <auto-generated />
using System;
using Indice_Academico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Indice_Academico.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211025022937_Intial")]
    partial class Intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Indice_Academico.Data.Entity.Score", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GradeLetter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradeNumber")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Career")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("StudentCode")
                        .HasMaxLength(8)
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.StudentSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ScoreInLetter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScoreInNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId")
                        .IsUnique();

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("Section")
                        .HasColumnType("int");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Score", b =>
                {
                    b.HasOne("Indice_Academico.Data.Entity.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Indice_Academico.Data.Entity.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.HasOne("Indice_Academico.Data.Entity.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.Navigation("Student");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.StudentSubject", b =>
                {
                    b.HasOne("Indice_Academico.Data.Entity.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Indice_Academico.Data.Entity.Subject", "Subject")
                        .WithOne()
                        .HasForeignKey("Indice_Academico.Data.Entity.StudentSubject", "SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Subject", b =>
                {
                    b.HasOne("Indice_Academico.Data.Entity.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Student", b =>
                {
                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("Indice_Academico.Data.Entity.Teacher", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}