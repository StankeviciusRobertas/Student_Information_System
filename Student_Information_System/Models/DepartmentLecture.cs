using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    public class DepartmentLecture
    {
        public int DepartmentId { get; set; }
        public int LectureId { get; set; }
        public Department Department { get; set; }
        public Lecture Lecture { get; set; }
    }
}
