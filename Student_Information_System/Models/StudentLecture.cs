using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    public class StudentLecture
    {
        public int StudentId { get; set; }
        public int LectureId { get; set; }

        public Student Student { get; set; }
        public Lecture Lecture { get; set; }
    }
}
