using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NudeAssignment.Entities;
using NudeAssignment.Models;

namespace NudeAssignment.Services
{
    public interface ICoverageItemService
    {
        IEnumerable<LKPCoverageItemCategory> GetCoverageItemCategories();
        IEnumerable<CustomerCategoriesItems> GetCustomerItems(long customerId);
        string AddCustomerItem(CoverageItem coverageItem);
        int RemoveCustomerItem(Guid itemId);
    }
}
