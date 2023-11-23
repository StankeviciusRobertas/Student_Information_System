using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Student> Students { get; set; }
        //navigacinis propertis nurodantis jog, departamentai gali tureti daug paskaitu. 
        public List<DepartmentLecture> DepartmentLectures { get; set; }
    }
}
