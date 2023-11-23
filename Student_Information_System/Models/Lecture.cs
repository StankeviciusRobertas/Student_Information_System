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
        //Navigacinis propertis nurodantis jog paskaita gali buti priskirta ir Studentui, ir departamentui. 
        public List<StudentLecture> StudentLectures { get; set; }
        public List<DepartmentLecture> DepartmentLectures { get; set; }
    }
}
