#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string Tfname { get; set; } = null!;
        public string Tlname { get; set; } = null!;
        public string Taddress { get; set; } = null!;
        public string Tphonenumber { get; set; }
        public bool Ishead { get; set; }
        public string Email { get; set; } = null!;
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; } = null!;
    }
}
