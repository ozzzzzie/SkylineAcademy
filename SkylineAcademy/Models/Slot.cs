#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Slot
    {
        public Slot()
        {
            ClassSchedules = new HashSet<ClassSchedule>();
            ClassroomAvailabilities = new HashSet<ClassroomAvailability>();
            TeacherAvailabilities = new HashSet<TeacherAvailability>();
        }

        public int SlotId { get; set; }
        public string WeekdayName { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<ClassroomAvailability> ClassroomAvailabilities { get; set; }
        public virtual ICollection<TeacherAvailability> TeacherAvailabilities { get; set; }
    }
}
