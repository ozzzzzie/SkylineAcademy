#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int? EnrollementId { get; set; } = null!;
        public int? Midterm { get; set; } = null;
        public int? Final { get; set; } = null;
        public int? Total { get; set; } = null;
        public bool? Passedcourse { get; set; } = null;
    }
}
