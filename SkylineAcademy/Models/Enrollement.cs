#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Enrollement
    {
        public int EnrollementId { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public byte[] EnrollementDate { get; set; } = null!;

        public virtual ClassSchedule Schedule { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
