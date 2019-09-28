using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NudeAssignment.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }
        public string Name { get; set; }
    }
}
