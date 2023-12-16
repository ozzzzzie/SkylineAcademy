#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class TeacherAvailability
    {
        public int TeacherAvailabilityId { get; set; }
        public int SlotId { get; set; }
        public int TeacherId { get; set; }
        public bool? Isavailable { get; set; }

        public virtual Slot Slot { get; set; } = null!;
    }
}
