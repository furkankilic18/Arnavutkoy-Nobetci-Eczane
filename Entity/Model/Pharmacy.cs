using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        public int DistrictId { get; set; }
        public District? District { get; set; }

        public ICollection<Duty>? Duties { get; set; }
    }
}
