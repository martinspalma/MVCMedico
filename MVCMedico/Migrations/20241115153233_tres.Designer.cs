﻿// <auto-generated />
using System;
using MVCMedico.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCMedico.Migrations
{
    [DbContext(typeof(MedicoDatabaseContext))]
    [Migration("20241115153233_tres")]
    partial class tres
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVCMedico.Models.Afiliado", b =>
                {
                    b.Property<int>("IdAfiliado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAfiliado"));

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("telefono")
                        .HasColumnType("int");

                    b.Property<int>("tipoPlan")
                        .HasColumnType("int");

                    b.HasKey("IdAfiliado");

                    b.ToTable("Afiliados");
                });

            modelBuilder.Entity("MVCMedico.Models.Cita", b =>
                {
                    b.Property<int>("IdCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCita"));

                    b.Property<int>("PrestadorMedicoIdPrestador")
                        .HasColumnType("int");

                    b.Property<bool>("estaDisponible")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("fechaCita")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("horaCita")
                        .HasColumnType("time");

                    b.HasKey("IdCita");

                    b.HasIndex("PrestadorMedicoIdPrestador");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("MVCMedico.Models.PrestadorMedico", b =>
                {
                    b.Property<int>("IdPrestador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrestador"));

                    b.Property<string>("DireccionMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<string>("MailMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatriculaProfesional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TelefonoMedico")
                        .HasColumnType("int");

                    b.HasKey("IdPrestador");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("MVCMedico.Models.Turno", b =>
                {
                    b.Property<int>("IdTurno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTurno"));

                    b.Property<int>("AfiliadoIdAfiliado")
                        .HasColumnType("int");

                    b.Property<int>("PrestadorMedicoIdPrestador")
                        .HasColumnType("int");

                    b.HasKey("IdTurno");

                    b.HasIndex("AfiliadoIdAfiliado");

                    b.HasIndex("PrestadorMedicoIdPrestador");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("MVCMedico.Models.Cita", b =>
                {
                    b.HasOne("MVCMedico.Models.PrestadorMedico", "PrestadorMedico")
                        .WithMany("Citas")
                        .HasForeignKey("PrestadorMedicoIdPrestador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrestadorMedico");
                });

            modelBuilder.Entity("MVCMedico.Models.Turno", b =>
                {
                    b.HasOne("MVCMedico.Models.Afiliado", "Afiliado")
                        .WithMany()
                        .HasForeignKey("AfiliadoIdAfiliado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCMedico.Models.PrestadorMedico", "PrestadorMedico")
                        .WithMany()
                        .HasForeignKey("PrestadorMedicoIdPrestador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Afiliado");

                    b.Navigation("PrestadorMedico");
                });

            modelBuilder.Entity("MVCMedico.Models.PrestadorMedico", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
