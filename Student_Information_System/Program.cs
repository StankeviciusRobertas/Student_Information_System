using Microsoft.EntityFrameworkCore;
using Student_Information_System.Models;

namespace Student_Information_System
{
    internal class Program
    {
        static void Main(string[] args)
        {         
            var dbContext = new StudentContext(new DbContextOptionsBuilder<StudentContext>()
                .UseSqlServer($"Server=localhost;Database=Student_Information_System;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options);

            string userInput = "";

            while (userInput.ToLower() != "q")
            {
                var menu = new FunctionMeniu(dbContext);
                menu.Functions();

                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "1":
                        menu.CreateDepartment();
                        break;
                    case "2":
                        menu.AddStudentToDepartment();
                        break;
                    case "3":
                        menu.AddLecturesToDepartment();
                        break;
                    case "4":
                        menu.CreateLectureAndAddToDepartment();
                        break;
                    case "5":
                        menu.CreateStudentWithExistingLectures();
                        break;
                    case "6":
                        menu.TransferStudentToAnotherDepartment();
                        break;
                    case "7":
                        menu.DisplayAllStudentsOfSelectedDepartment();
                        break;
                    case "8":
                        menu.DisplayAllLecturesOfSelectedDepartment();
                        break;
                    case "9":
                        menu.DisplayAllLecturesByStudent();
                        break;
                    case "q":
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid option.");
                        break;
                }
            }

            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();

            //var transport = new Department { DepartmentName = "Transporto inzinerija" };
            //var construction = new Department { DepartmentName = "Statybu inzinerija" };
            //var it = new Department { DepartmentName = "Informaciniu technologiju" };
            //dbContext.Departments.AddRange(transport, construction, it);
            //dbContext.SaveChanges();

            //var matematic = new Lecture { LectureName = "Matematika" };
            //var physics = new Lecture { LectureName = "Fizika" };
            //var informatics = new Lecture { LectureName = "Informaciniu technologiju" };

            //dbContext.Lectures.AddRange(matematic, physics, informatics);
            //dbContext.SaveChanges();

            //var student = new Student { FirstName = "Robertas", LastName = "Stankevicius", Email = "Robertas.Stankevicius@gmail.com", Department = transport };
            //var student2 = new Student { FirstName = "Petras", LastName = "Petraitis", Email = "petras.petraitis@gmail.com", Department = construction };
            //var student3 = new Student { FirstName = "Antanas", LastName = "Antanaitis", Email = "Antanas.Antanaitis@gmail.com", Department = it };

            //dbContext.Students.AddRange(student, student2, student3);
            //dbContext.SaveChanges();

            //dbContext.StudentLectures.Add(new StudentLecture { Student = student, Lecture = matematic });
            //dbContext.StudentLectures.Add(new StudentLecture { Student = student, Lecture = physics });
            //dbContext.StudentLectures.Add(new StudentLecture { Student = student, Lecture = informatics });

            //dbContext.StudentLectures.Add(new StudentLecture { Student = student2, Lecture = physics });
            //dbContext.StudentLectures.Add(new StudentLecture { Student = student2, Lecture = informatics });

            //dbContext.StudentLectures.Add(new StudentLecture { Student = student3, Lecture = informatics });

            //dbContext.SaveChanges();

            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = transport, Lecture = matematic });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = transport, Lecture = physics });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = transport, Lecture = informatics });

            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = construction, Lecture = matematic });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = construction, Lecture = physics });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = construction, Lecture = informatics });

            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = it, Lecture = matematic });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = it, Lecture = physics });
            //dbContext.DepartmentLectures.Add(new DepartmentLecture { Department = it, Lecture = informatics });

            //dbContext.SaveChanges();
        }
    }
}