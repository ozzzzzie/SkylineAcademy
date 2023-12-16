#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class ClassroomAvailability
    {
        public int ClassroomAvailabilityId { get; set; }
        public int SlotId { get; set; }
        public int ClassroomId { get; set; }
        public bool? Isavailable { get; set; }

        public virtual Classroom Classroom { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
    }
}
