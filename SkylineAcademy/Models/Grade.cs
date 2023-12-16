#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public string EnrollementId { get; set; } = null!;
        public int Midterm { get; set; }
        public int Final { get; set; }
        public int Total { get; set; }
    }
}
