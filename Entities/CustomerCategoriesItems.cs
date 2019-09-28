using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NudeAssignment.Models;

namespace NudeAssignment.Entities
{
    public class CustomerCategoriesItems
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryValue
        {
            get { return CoverageItems.Sum(c => c.Value); }
        }

        public IEnumerable<CoverageItem> CoverageItems { get; set; }
    }
}
