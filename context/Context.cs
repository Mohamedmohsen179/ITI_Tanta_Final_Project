using ITI_Tanta_Final_Project.Models;
using Microsoft.EntityFrameworkCore;


namespace ITI_Tanta_Final_Project.context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Models.Course> Courses { get; set; }
        public DbSet<Models.Session> Sessions { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Grade> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();




            modelBuilder.Entity<User>()
                .HasMany(u => u.studiedCourses)
                 .WithMany(c => c.Trainees)
                  .UsingEntity(j => j.ToTable("UserCourses"));
                  

            modelBuilder.Entity<User>()
                .HasMany(u => u.TeachingCourses)
                 .WithOne(c => c.Instructor)
                  .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
              .HasMany(u => u.Grades)
               .WithOne(g => g.Trainee)
                .HasForeignKey(g => g.TraineeId)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Course>()
                .HasMany(c => c.Sessions)
                 .WithOne(s => s.Course)
                  .HasForeignKey(s => s.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
             .HasMany(s => s.Grades)
               .WithOne(g => g.Session)
                .HasForeignKey(g => g.SessionId)
                 .OnDelete(DeleteBehavior.Restrict);



        }
    }

}
