#nullable disable

using System;
using System.Collections.Generic;

namespace SkylineAcademy.Models
{
    public partial class Administrator
    {
        public int AdministratorId { get; set; }
        public string Afname { get; set; } = null!;
        public string Alname { get; set; } = null!;
        public string Aaddress { get; set; } = null!;
        public string Aphonenumber { get; set; }
        public int JobId { get; set; }
        public string Aemail { get; set; } = null!;

        public virtual Job Job { get; set; } = null!;
    }
}
