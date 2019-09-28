using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NudeAssignment.Models
{
    public class CustomerItem
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public Guid ItemId { get; set; }
    }
}
