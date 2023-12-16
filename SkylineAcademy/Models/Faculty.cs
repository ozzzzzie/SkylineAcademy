#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Majors = new HashSet<Major>();
            Teachers = new HashSet<Teacher>();
        }

        public int FacultyId { get; set; }
        public string Fname { get; set; } = null!;

        public virtual ICollection<Major> Majors { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
