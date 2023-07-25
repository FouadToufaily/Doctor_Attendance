using System;
using System.Collections.Generic;
using Doctor_Attendance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Doctor_Attendance.Services
{
    public partial class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-O8T7K8E\\SQLEXPRESS;Initial Catalog=Doctor_Attendance;Integrated Security=True");
            }
        }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.AttId);

                entity.ToTable("ATTENDANCE");

                entity.Property(e => e.AttId).HasColumnName("ATT_ID");

                entity.Property(e => e.Attended).HasColumnName("ATTENDED");

                entity.Property(e => e.Comments)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE");

                entity.Property(e => e.DepId).HasColumnName("DEP_ID");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.NbHours).HasColumnName("NB_HOURS");

                entity.Property(e => e.Published).HasColumnName("PUBLISHED");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ATTENDAN_ATTENDANC_DEPARTME");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ATTENDAN_ATTENDANC_DOCTOR");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .IsClustered(false);

                entity.ToTable("CATEGORY");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .IsClustered(false);

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DepId).HasColumnName("DEP_ID");

                entity.Property(e => e.DepName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DEP_NAME");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Nbdoctors).HasColumnName("NBDOCTORS");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_DEPARTME_COORDINAT_DOCTOR");

                entity.HasMany(d => d.Sections)
                    .WithMany(p => p.Deps)
                    .UsingEntity<Dictionary<string, object>>(
                        "BelongTo",
                        l => l.HasOne<Section>().WithMany().HasForeignKey("Sectionid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BELONG_T_BELONG_TO_SECTION"),
                        r => r.HasOne<Department>().WithMany().HasForeignKey("DepId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BELONG_T_BELONG_TO_DEPARTME"),
                        j =>
                        {
                            j.HasKey("DepId", "Sectionid");

                            j.ToTable("BELONG_TO");

                            j.IndexerProperty<int>("DepId").HasColumnName("DEP_ID");

                            j.IndexerProperty<int>("Sectionid").HasColumnName("SECTIONID");
                        });
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId)
                    .IsClustered(false);

                entity.ToTable("DOCTOR");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_DOCTOR_IS_OF_TYP_CATEGORY");

                entity.HasMany(d => d.Deps)
                    .WithMany(p => p.Doctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "Teach",
                        l => l.HasOne<Department>().WithMany().HasForeignKey("DepId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TEACHES_TEACHES2_DEPARTME"),
                        r => r.HasOne<Doctor>().WithMany().HasForeignKey("DoctorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TEACHES_TEACHES_DOCTOR"),
                        j =>
                        {
                            j.HasKey("DoctorId", "DepId");

                            j.ToTable("TEACHES");

                            j.IndexerProperty<int>("DoctorId").HasColumnName("DOCTOR_ID");

                            j.IndexerProperty<int>("DepId").HasColumnName("DEP_ID");
                        });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .IsClustered(false);

                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.DepId).HasColumnName("DEP_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK_EMPLOYEE_WORK_IN_DEPARTME");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.Facultyid)
                    .IsClustered(false);

                entity.ToTable("FACULTY");

                entity.Property(e => e.Facultyid).HasColumnName("FACULTYID");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_FACULTY_MANAGES_DOCTOR");

                entity.HasMany(d => d.Sections)
                    .WithMany(p => p.Faculties)
                    .UsingEntity<Dictionary<string, object>>(
                        "Ha",
                        l => l.HasOne<Section>().WithMany().HasForeignKey("Sectionid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_HAS_HAS2_SECTION"),
                        r => r.HasOne<Faculty>().WithMany().HasForeignKey("Facultyid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_HAS_HAS_FACULTY"),
                        j =>
                        {
                            j.HasKey("Facultyid", "Sectionid");

                            j.ToTable("HAS");

                            j.IndexerProperty<int>("Facultyid").HasColumnName("FACULTYID");

                            j.IndexerProperty<int>("Sectionid").HasColumnName("SECTIONID");
                        });
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Permissionid)
                    .IsClustered(false);

                entity.ToTable("PERMISSIONS");

                entity.Property(e => e.Permissionid)
                    .ValueGeneratedNever()
                    .HasColumnName("PERMISSIONID");

                entity.Property(e => e.AddAttendence).HasColumnName("ADD_ATTENDENCE");

                entity.Property(e => e.DeleteAttendence).HasColumnName("DELETE_ATTENDENCE");

                entity.Property(e => e.UpdateAttendence).HasColumnName("UPDATE_ATTENDENCE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .IsClustered(false);

                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.Permissionid).HasColumnName("PERMISSIONID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.Permissionid)
                    .HasConstraintName("FK_ROLE_HAS_PERMI_PERMISSI");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.Sectionid)
                    .IsClustered(false);

                entity.ToTable("SECTION");

                entity.Property(e => e.Sectionid).HasColumnName("SECTIONID");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_SECTION_DIRECTS_DOCTOR");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_USER")
                    .IsClustered(false);

                entity.ToTable("USERS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_MODIFIED");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASSWORD");

                entity.Property(e => e.UserUsername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_USERNAME");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_USER_USES_DOCTOR");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_USER_RELATIONS_EMPLOYEE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_USER_HAS_ROLE_ROLE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
