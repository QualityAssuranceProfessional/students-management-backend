using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class City
    {
        public City()
        {
            Regions = new HashSet<Region>();
        }

        public int CityId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
