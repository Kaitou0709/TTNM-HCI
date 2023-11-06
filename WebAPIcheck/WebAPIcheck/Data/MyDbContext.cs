using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace WebAPIcheck.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Grade> Grades { get; set; }    
        public DbSet<Students> Students { get; set; }
        public DbSet<LessonTime> LessonTime { get; set; }
        public DbSet<Semesters> Semesters { get; set; } 
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<SubjectsClass> SubjectClasses { get; set; }
        public DbSet<SemestersSubject> SemestersSubject { get; set; }
        public DbSet<ReSubjects> ReSubjects { get; set; }
        public DbSet<ExamSubjects> ExamSubjects { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Tuitionfee> Tuitionfees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            });
            modelBuilder.Entity<Students>()
            .Property(s => s.IdStudent)
            .ValueGeneratedNever();
        }
    }
}
