#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Major
    {
        public Major()
        {
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }

        public int MajorId { get; set; }
        public string Mname { get; set; } = null!;
        public string Mdescription { get; set; } = null!;
        public int? FacultyId { get; set; }

        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
