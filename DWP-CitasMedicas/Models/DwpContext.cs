using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DWP_CitasMedicas.Models;

public partial class DwpContext : DbContext
{
    public DwpContext()
    {
    }

    public DwpContext(DbContextOptions<DwpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agenda> Agenda { get; set; }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agenda>(entity =>
        {
            entity.HasKey(e => e.IdAgenda).HasName("PK__AGENDA__FACC499EF571D2F2");

            entity.ToTable("AGENDA");

            entity.Property(e => e.FechaCita).HasColumnType("datetime");
            entity.Property(e => e.TipodeServicio)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Agenda)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__AGENDA__IdDoctor__403A8C7D");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__CITA__814F31263AB96C1A");

            entity.ToTable("CITA");

            entity.Property(e => e.IdCita).HasColumnName("idCita");
            entity.Property(e => e.IdAgenda).HasColumnName("idAgenda");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.MotivoDeConsulta).HasColumnType("text");

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK__CITA__idAgenda__4316F928");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__CITA__idDoctor__44FF419A");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__CITA__idPaciente__440B1D61");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__D59466424DCBB0B1");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => e.Correo, "UQ__CLIENTE__60695A19C51BDAB0").IsUnique();

            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__DOCTOR__418956C3E94F57E7");

            entity.ToTable("DOCTOR");

            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.CedulaProfesional)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Horario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__PACIENTE__F48A08F2318C39F1");

            entity.ToTable("PACIENTE");

            entity.HasIndex(e => e.IdCliente, "UQ__PACIENTE__D594664374830F3B").IsUnique();

            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estatura).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDeEmergencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Peso).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TipodeSangre)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithOne(p => p.Paciente)
                .HasForeignKey<Paciente>(d => d.IdCliente)
                .HasConstraintName("FK__PACIENTE__IdClie__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
