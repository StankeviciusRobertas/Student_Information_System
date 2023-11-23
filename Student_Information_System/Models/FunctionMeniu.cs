using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    public class FunctionMeniu
    {
        public readonly StudentContext dbContext;
        public FunctionMeniu(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public Department CreateDepartment()
        {
            try
            {
                Console.WriteLine("Enter the name of the department:");
                string departmentName = Console.ReadLine();

                Department department = new Department { DepartmentName = departmentName };
                dbContext.Departments.Add(department);
                dbContext.SaveChanges();

                Console.WriteLine($"Department '{departmentName}' created successfully with ID {department.DepartmentId}");
                Console.WriteLine("Press Enter to proceed");
                Console.ReadKey();
                Console.Clear();
                return department;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public void AddStudentToDepartment()
        {
            try
            {


                Console.WriteLine("Enter the details of the student:");
                Console.Write("First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();

                var department = SelectDepartment();
                var student = new Student { FirstName = firstName, LastName = lastName, Email = email, Department = department };

                dbContext.Students.Add(student);
                dbContext.SaveChanges();

                Console.WriteLine($"Student '{firstName} {lastName}' added to the department '{department.DepartmentName}'");
                Console.WriteLine("Press Enter to proceed");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erorr: {ex.Message}");
            }
        }
        public void AddLecturesToDepartment()
        {
            var department = SelectDepartment();

            Console.WriteLine("Enter the IDs of lectures (comma-separated):");
            DisplayAllLectures();

            //ivedinejam paskaitos ID, skeldami per kableli
            string[] lectureIdsInput = Console.ReadLine().Split(',');

            foreach (var lectureIdInput in lectureIdsInput)
            {
                if (int.TryParse(lectureIdInput, out int lectureId))
                {
                    var existingLecture = dbContext.Lectures.Find(lectureId);

                    if (existingLecture == null)
                    {
                        Console.WriteLine($"Lecture with ID {lectureId} doesn't exist. Do you want to ADD? y/n");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "y")
                        {
                            existingLecture = new Lecture { LectureId = lectureId, LectureName = $"Lecture {lectureId}" };
                            dbContext.Lectures.Add(existingLecture);
                            dbContext.SaveChanges();
                            Console.WriteLine($"Lecture with ID {lectureId} created");
                        }
                        else
                        {
                            Console.WriteLine($"Lecture with ID {lectureId} not created. ");
                        }
                    }

                    var departmentLecture = new DepartmentLecture { Department = department, Lecture = existingLecture };
                    dbContext.DepartmentLectures.Add(departmentLecture);
                }
                else
                {
                    Console.WriteLine($"Invalid input for lecture ID: {lectureIdInput}");
                }
            }

            dbContext.SaveChanges();
            Console.WriteLine($"Lectures added to the department '{department.DepartmentName}'");

            Console.WriteLine("Press Enter to proceed");
            Console.ReadKey();
            Console.Clear();
        }
        public Lecture CreateLectureAndAddToDepartment()
        {
            Console.WriteLine("Enter the name of the lecture:");
            string lectureName = Console.ReadLine();

            DisplayAllDepartments();
            var department = SelectDepartment();

            Lecture lecture = new Lecture { LectureName = lectureName };
            dbContext.Lectures.Add(lecture);

            if (department != null)
            {
                // Pridedam paskaita i departamenta per jungiamaja lentele. 
                var departmentLecture = new DepartmentLecture { Department = department, Lecture = lecture };
                dbContext.DepartmentLectures.Add(departmentLecture);
            }

            dbContext.SaveChanges();

            Console.WriteLine($"Lecture '{lectureName}' created successfully with ID {lecture.LectureId}");
            Console.WriteLine("Press Enter to proceed");
            Console.ReadKey();
            Console.Clear();

            return lecture;
        }
        public void CreateStudentWithExistingLectures()
        {
            Console.WriteLine("Enter the details of the student:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            var department = SelectDepartment();

            Console.WriteLine("Enter the names of lectures (comma-separated):");
            DisplayAllLectures();
            string[] lectureNames = Console.ReadLine().Split(',');

            var student = new Student { FirstName = firstName, LastName = lastName, Email = email, Department = department };

            foreach (var lectureName in lectureNames)
            {
                var existingLecture = dbContext.Lectures.FirstOrDefault(l => l.LectureName == lectureName.Trim());

                if (existingLecture == null)
                {
                    Console.WriteLine($"Lecture '{lectureName}' not found. Please create the lecture first.");
                    return;
                }

                var studentLecture = new StudentLecture { Student = student, Lecture = existingLecture };
                dbContext.StudentLectures.Add(studentLecture);
            }

            dbContext.SaveChanges();
            Console.WriteLine($"Student '{firstName} {lastName}' added to the department '{department.DepartmentName}' with existing lectures.");
        }
        public void TransferStudentToAnotherDepartment()
        {
            var student = SelectStudent();

            if (student != null)
            {
                var newDepartment = SelectDepartment();

                // isrenkam ir i strinam priskirtas paskaitas studentui per jungiamaja lentele pagal studento Id
                var studentLecturesToRemove = dbContext.StudentLectures
                    .Where(sl => sl.Student.StudentId == student.StudentId)
                    .ToList();

                dbContext.StudentLectures.RemoveRange(studentLecturesToRemove);
                dbContext.SaveChanges();

                // Selectinam paskaitas kurios pridetos per jungiama lentele prie naujo departamento pagal departamento Id
                var newDepartmentLectures = dbContext.DepartmentLectures
                    .Where(dl => dl.Department.DepartmentId == newDepartment.DepartmentId)
                    .Select(dl => dl.Lecture)
                    .ToList();

                // priskiriam tam studentui naujo departamento paskaitas
                foreach (var lecture in newDepartmentLectures)
                {
                    var studentLecture = new StudentLecture { Student = student, Lecture = lecture };
                    dbContext.StudentLectures.Add(studentLecture);
                }

                // Atnaujinam studento departamenta
                student.Department = newDepartment;
                dbContext.SaveChanges();

                Console.WriteLine($"Student '{student.FirstName} {student.LastName}' transferred to the department '{newDepartment.DepartmentName}'");
                Console.WriteLine("Press Enter to proceed");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void DisplayAllStudentsOfSelectedDepartment()
        {
            DisplayAllDepartments();
            var department = SelectDepartment();
            var students = dbContext.Students.Where(s => s.Department.DepartmentId == department.DepartmentId).ToList();

            Console.WriteLine($"Students of department '{department.DepartmentName}':");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} - {student.Email}");
            }
        }
        public void DisplayAllLecturesOfSelectedDepartment()
        {
            var department = SelectDepartment();
            var lectures = dbContext.DepartmentLectures
                .Where(dl => dl.Department.DepartmentId == department.DepartmentId)
                .Select(dl => dl.Lecture)
                .ToList();

            Console.WriteLine($"Lectures of department '{department.DepartmentName}':");
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"{lecture.LectureName}");
            }
        }
        public void DisplayAllLecturesByStudent()
        {
            var student = SelectStudent();

            if (student != null)
            {
                var lectures = dbContext.StudentLectures
                    .Where(sl => sl.Student.StudentId == student.StudentId)
                    .Select(sl => sl.Lecture)
                    .ToList();

                Console.WriteLine($"Lectures by student '{student.FirstName} {student.LastName}':");
                foreach (var lecture in lectures)
                {
                    Console.WriteLine($"{lecture.LectureName}");
                }
            }
        }

        public Student SelectStudent()
        {
            DisplayAllStudents();
            Console.WriteLine("Enter the ID of the student:");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                var student = dbContext.Students.Find(studentId);

                if (student != null)
                {
                    return student;
                }
                else
                {
                    Console.WriteLine("Student not found. Please enter a valid student ID.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid student ID.");
                return null;
            }
        }       

        public Department SelectDepartment()
        {
            Console.WriteLine("");
            DisplayAllDepartments();
            Console.WriteLine("Enter the ID of the department:");
            if (int.TryParse(Console.ReadLine(), out int departmentId))
            {
                var department = dbContext.Departments.Find(departmentId);

                if (department != null)
                {
                    return department;
                }
                else
                {
                    Console.WriteLine($"Department with ID {departmentId} not found.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Invalid input for department ID.");
                return null;
            }
        }        
        
        public void DisplayAllDepartments()
        {
            var departments = dbContext.Departments.ToList();

            Console.WriteLine("All Departments:");
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentId}. {department.DepartmentName}");
            }
        }
        public void DisplayAllLectures()
        {
            var lectures = dbContext.Lectures.ToList();

            Console.WriteLine("All Lectures:");
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"{lecture.LectureId}. {lecture.LectureName}");
            }
        }
        public void DisplayAllStudents()
        {
            var students = dbContext.Students.ToList();

            Console.WriteLine("All Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId}. {student.FirstName} {student.LastName} - {student.Email}");
            }
        }
        public void Functions()
        {
            Console.WriteLine("Welcome to the students Informations system");
            Console.WriteLine("Select action: ");
            Console.WriteLine("");
            Console.WriteLine("1. Create Department.");
            Console.WriteLine("2. Add student to department.");
            Console.WriteLine("3. Add lectures to department.");
            Console.WriteLine("4. Create Lecture and add to department. ");
            Console.WriteLine("5. Create student and add to department and add lectures.");
            Console.WriteLine("6. Transfer the student to another department");
            Console.WriteLine("7. Show all student of selected department");
            Console.WriteLine("8. Show all lectures of selected department");
            Console.WriteLine("9. Show all lectures of selected student. ");
            Console.WriteLine("10. Write 'q' to EXIT");
        }

    }
}
