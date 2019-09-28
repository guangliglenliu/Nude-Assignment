using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NudeAssignment.Models
{
    public class CoverageItem
    {
        [Key]
        public Guid ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
