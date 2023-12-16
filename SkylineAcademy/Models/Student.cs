#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollements = new HashSet<Enrollement>();
        }

        public int StudentId { get; set; }
        public string Sfname { get; set; } = null!;
        public string Slname { get; set; } = null!;
        public string Saddress { get; set; } = null!;
        public string Sphonenumber { get; set; }
        public int MajorId { get; set; }
        public string Semail { get; set; } = null!;

        public virtual Major Major { get; set; } = null!;
        public virtual ICollection<Enrollement> Enrollements { get; set; }
    }
}
