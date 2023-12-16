#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Prerequisite
    {
        public int CourseId { get; set; }
        public string PrerequisiteId { get; set; } = null!;
    }
}
