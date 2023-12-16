#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string Cname { get; set; } = null!;
        public int? MajorId { get; set; }
        public int Credits { get; set; }

        public virtual Major? Major { get; set; }
    }
}
