#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class ClassSchedule
    {
        public ClassSchedule()
        {
            Enrollements = new HashSet<Enrollement>();
        }

        public int ScheduleId { get; set; }
        public string CourseId { get; set; } = null!;
        public string AdministratorId { get; set; } = null!;
        public string TeacherId { get; set; } = null!;
        public string ClassroomId { get; set; } = null!;
        public int SlotId { get; set; }
        public string Semester { get; set; } = null!;
        public string Academicyear { get; set; } = null!;

        public virtual Slot Slot { get; set; } = null!;
        public virtual ICollection<Enrollement> Enrollements { get; set; }
    }
}
