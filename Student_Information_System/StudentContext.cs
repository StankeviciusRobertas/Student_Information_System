using Microsoft.EntityFrameworkCore;
using Student_Information_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base()
        {

        }
        public StudentContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentLecture> StudentLectures { get; set; }
        public DbSet<DepartmentLecture> DepartmentLectures { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //cia mes konfiguruojame sarysi many to many Studentas turi daug paskaitu
            modelBuilder.Entity<StudentLecture>()
                .HasKey(sl => new { sl.StudentId, sl.LectureId }); // sujungta su dviem raktais

            modelBuilder.Entity<StudentLecture>()                  //joininam lenteles
                .HasOne(sl => sl.Student)                          // Studenta jungiam prie paskaitos
                .WithMany(b => b.StudentLectures)                  // prie bendros lentos jungiam studenta
                .HasForeignKey(sl => sl.StudentId);                // pagal student id

            modelBuilder.Entity<StudentLecture>()                  // join lenta
                .HasOne(dl => dl.Lecture)                          // Paskaita joininam prie Studento
                .WithMany(c => c.StudentLectures)                  // prie bendros lentos jungiam kategorija
                .HasForeignKey(sl => sl.LectureId);                // pagal Paskaitos id

            //

            modelBuilder.Entity<DepartmentLecture>()
                .HasKey(dl => new { dl.DepartmentId, dl.LectureId }); // sujungta su dviem raktais

            modelBuilder.Entity<DepartmentLecture>()               //joininam lenteles
                .HasOne(dl => dl.Department)                       // Departamenta jungiam prie Paskaitos
                .WithMany(b => b.DepartmentLectures)               // prie bendros lentos jungiam De[artamenta
                .HasForeignKey(dl => dl.DepartmentId);             // pagal Departamento id

            modelBuilder.Entity<DepartmentLecture>()               // join lentas
                .HasOne(dl => dl.Lecture)                          //Paskaita joininam prie departamento
                .WithMany(c => c.DepartmentLectures)               // prie bendros lentos jungiam Paskaita
                .HasForeignKey(sl => sl.LectureId);                // pagal paskaitos id
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server=localhost;Database=Student_Information_System;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
