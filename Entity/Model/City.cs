using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
