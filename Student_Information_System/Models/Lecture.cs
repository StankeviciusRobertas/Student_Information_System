using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public List<Department> Departments { get; set; }
        public List<StudentLecture> StudentLectures { get; set; }
        public List<DepartmentLecture> DepartmentLectures { get; set; }
    }
}
