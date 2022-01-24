using Indice_Academico.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Indice_Academico.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Student> Student { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<Score> Score { get; set; }

        public DbSet<StudentSubject> StudentSubjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

    
        protected override void OnModelCreating(ModelBuilder theModelBuilder)
        {
            theModelBuilder.Entity<Student>(aEntity => 
            {
                aEntity.HasKey(e => e.Id);
                aEntity.Property(e => e.Firstname).HasMaxLength(45).IsRequired();
                aEntity.Property(e => e.Lastname).HasMaxLength(45).IsRequired();
                aEntity.Property(e => e.StudentCode).HasMaxLength(8).IsRequired();
                aEntity.Property(e => e.Career).HasMaxLength(50).IsRequired();
                aEntity.HasMany(e => e.StudentSubjects).WithOne(e => e.Student).HasForeignKey(e => e.StudentId);
            });

            theModelBuilder.Entity<Teacher>(aEntity => 
            {
                aEntity.HasKey(e => e.Id);
                aEntity.Property(e => e.Firstname).HasMaxLength(45).IsRequired();
                aEntity.Property(e => e.Lastname).HasMaxLength(45).IsRequired();
                aEntity.HasMany(e => e.Subjects).WithOne(e => e.Teacher).HasForeignKey(e => e.TeacherId);
            });

            theModelBuilder.Entity<Subject>(aEntity =>
            {
                aEntity.HasKey(e => e.Id);
                aEntity.Property(e => e.Name).HasMaxLength(45).IsRequired();
                aEntity.Property(e => e.SubjectCode).HasMaxLength(20).IsRequired();
                aEntity.Property(e => e.Credit).IsRequired();
                aEntity.Property(e => e.Section).IsRequired();
                aEntity.HasOne(e => e.Teacher).WithMany(e => e.Subjects).HasForeignKey(e => e.TeacherId);
            });

            theModelBuilder.Entity<StudentSubject>(aEntity => 
            {
                aEntity.HasKey(e => e.Id);
                aEntity.Property(e => e.ScoreInLetter);
                aEntity.Property(e => e.ScoreInNumber);
                aEntity.HasOne(e => e.Student).WithMany(x => x.StudentSubjects).HasForeignKey(e => e.StudentId);
                aEntity.HasOne(e => e.Subject).WithOne();
            });
        }

    }
}
