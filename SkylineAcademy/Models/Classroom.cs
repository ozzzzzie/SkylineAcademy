#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            ClassroomAvailabilities = new HashSet<ClassroomAvailability>();
        }

        public int ClassroomId { get; set; }
        public int Capacity { get; set; }
        public string Facilities { get; set; } = null!;

        public virtual ICollection<ClassroomAvailability> ClassroomAvailabilities { get; set; }
    }
}
