using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class Duty
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
