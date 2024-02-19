using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoFinal.Models;

namespace ProyectoFinal.Context
{
    public partial class EduAsyncHubContext : DbContext
    {
        public EduAsyncHubContext()
        {
        }

        public EduAsyncHubContext(DbContextOptions<EduAsyncHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignacione> Asignaciones { get; set; } = null!;
        public virtual DbSet<Asistencium> Asistencia { get; set; } = null!;
        public virtual DbSet<Auditorium> Auditoria { get; set; } = null!;
        public virtual DbSet<Calificacione> Calificaciones { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<CarrerasMateria> CarrerasMaterias { get; set; } = null!;
        public virtual DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public virtual DbSet<EstudianteMaterium> EstudianteMateria { get; set; } = null!;
        public virtual DbSet<Materia> Materias { get; set; } = null!;
        public virtual DbSet<ProfesorMaterium> ProfesorMateria { get; set; } = null!;
        public virtual DbSet<Profesore> Profesores { get; set; } = null!;
        public virtual DbSet<RespuestasEstudiante> RespuestasEstudiantes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignacione>(entity =>
            {
                entity.HasKey(e => e.AsignacionId)
                    .HasName("PK__Asignaci__D82B5BB75C0748D0");

                entity.Property(e => e.AsignacionId).HasColumnName("AsignacionID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FechaPublicacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVencimiento).HasColumnType("date");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.Asignaciones)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__Asignacio__Mater__6C190EBB");

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Asignaciones)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__Asignacio__Profe__6D0D32F4");
            });

            modelBuilder.Entity<Asistencium>(entity =>
            {
                entity.HasKey(e => e.AsistenciaId)
                    .HasName("PK__Asistenc__72710F45D04CC880");

                entity.Property(e => e.AsistenciaId).HasColumnName("AsistenciaID");

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.FechaAsistencia).HasColumnType("date");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Asistenci__Estud__73BA3083");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__Asistenci__Mater__74AE54BC");

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__Asistenci__Profe__75A278F5");
            });

            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.HasKey(e => e.AuditoriaId)
                    .HasName("PK__Auditori__095694E3C51A73FD");

                entity.Property(e => e.AuditoriaId).HasColumnName("AuditoriaID");

                entity.Property(e => e.Accion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Auditoria)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Auditoria__Usuar__7E37BEF6");
            });

            modelBuilder.Entity<Calificacione>(entity =>
            {
                entity.HasKey(e => e.CalificacionId)
                    .HasName("PK__Califica__4CF54ABE8892308A");

                entity.Property(e => e.CalificacionId).HasColumnName("CalificacionID");

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.FechaPublicacion).HasColumnType("date");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Calificac__Estud__787EE5A0");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__Calificac__Mater__797309D9");

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__Calificac__Profe__7A672E12");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.Property(e => e.CarreraId)
                    .ValueGeneratedNever()
                    .HasColumnName("CarreraID");

                entity.Property(e => e.NombreCarrera)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarrerasMateria>(entity =>
            {
                entity.HasKey(e => e.MateriaCarreraId)
                    .HasName("PK__Carreras__DB6CB47D42B2CBFE");

                entity.Property(e => e.MateriaCarreraId).HasColumnName("MateriaCarreraID");

                entity.Property(e => e.CarreraId).HasColumnName("CarreraID");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.CarrerasMateria)
                    .HasForeignKey(d => d.CarreraId)
                    .HasConstraintName("FK__CarrerasM__Carre__0F624AF8");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.CarrerasMateria)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__CarrerasM__Mater__10566F31");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasIndex(e => e.UsuarioId, "UQ__Estudian__2B3DE7991BE48467")
                    .IsUnique();

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.CarreraId).HasColumnName("CarreraID");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.CarreraId)
                    .HasConstraintName("FK__Estudiant__Carre__571DF1D5");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.Estudiante)
                    .HasForeignKey<Estudiante>(d => d.UsuarioId)
                    .HasConstraintName("FK__Estudiant__Usuar__5629CD9C");
            });

            modelBuilder.Entity<EstudianteMaterium>(entity =>
            {
                entity.HasKey(e => e.InscripcionMateriaId)
                    .HasName("PK__Estudian__6352D9CD2BA52A8A");

                entity.Property(e => e.InscripcionMateriaId).HasColumnName("InscripcionMateriaID");

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.EstudianteMateria)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Estudiant__Estud__03F0984C");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.EstudianteMateria)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__Estudiant__Mater__04E4BC85");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.NombreMateria)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfesorMaterium>(entity =>
            {
                entity.HasKey(e => e.AsignacionMateriaId)
                    .HasName("PK__Profesor__9A018F135DCBF710");

                entity.Property(e => e.AsignacionMateriaId).HasColumnName("AsignacionMateriaID");

                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.ProfesorMateria)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("FK__ProfesorM__Mater__0C85DE4D");

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.ProfesorMateria)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__ProfesorM__Profe__0B91BA14");
            });

            modelBuilder.Entity<Profesore>(entity =>
            {
                entity.HasKey(e => e.ProfesorId)
                    .HasName("PK__Profesor__4DF3F028724623A4");

                entity.HasIndex(e => e.UsuarioId, "UQ__Profesor__2B3DE79969B08C1E")
                    .IsUnique();

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.Profesore)
                    .HasForeignKey<Profesore>(d => d.UsuarioId)
                    .HasConstraintName("FK__Profesore__Usuar__5AEE82B9");
            });

            modelBuilder.Entity<RespuestasEstudiante>(entity =>
            {
                entity.HasKey(e => e.RespuestaId)
                    .HasName("PK__Respuest__31F7FC31C5B1533C");

                entity.Property(e => e.RespuestaId).HasColumnName("RespuestaID");

                entity.Property(e => e.AsignacionId).HasColumnName("AsignacionID");

                entity.Property(e => e.ComentariosProfesor).HasColumnType("text");

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.Respuesta).HasColumnType("text");

                entity.HasOne(d => d.Asignacion)
                    .WithMany(p => p.RespuestasEstudiantes)
                    .HasForeignKey(d => d.AsignacionId)
                    .HasConstraintName("FK__Respuesta__Asign__70DDC3D8");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.RespuestasEstudiantes)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Respuesta__Estud__6FE99F9F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("PK__Roles__F92302D15D934590");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnName("RolID");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__531402F379B34BCC")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionBreve)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FotoPerfil)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Habilidades)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Intereses)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("RolID");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK__Usuarios__RolID__5070F446");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
