#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Department
    {
        public Department()
        {
            Jobs = new HashSet<Job>();
        }

        public int DepartmentId { get; set; }
        public string Dname { get; set; } = null!;
        public string Ddescription { get; set; } = null!;

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
