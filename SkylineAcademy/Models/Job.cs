#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Job
    {
        public Job()
        {
            Administrators = new HashSet<Administrator>();
        }

        public int JobId { get; set; }
        public string Jname { get; set; } = null!;
        public string Jdescription { get; set; } = null!;
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Administrator> Administrators { get; set; }
    }
}
