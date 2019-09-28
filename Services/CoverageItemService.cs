using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NudeAssignment.Entities;
using NudeAssignment.Models;

namespace NudeAssignment.Services
{
    public class CoverageItemService : ICoverageItemService
    {
        _CoverageDBContext _dbContext = null;

        public CoverageItemService(_CoverageDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<LKPCoverageItemCategory> GetCoverageItemCategories()
        {
            var categories = _dbContext.LKPCoverageItemCategories;
            return categories;
        }

        public IEnumerable<CustomerCategoriesItems> GetCustomerItems(long customerId = 1)
        {
            List<CustomerCategoriesItems> items = new List<CustomerCategoriesItems>();

            var customerItems = _dbContext.CoverageItems
                .Where(item => _dbContext.CustomerItems
                    .Where(ci => ci.CustomerId == customerId)
                    .Select(i => i.ItemId).Contains(item.ItemId));

            var categoryIds = customerItems
                .OrderBy(item => item.Name)
                .Select(item => item.CategoryId)
                .Distinct();

            foreach(var cid in categoryIds)
            {
                var item = new CustomerCategoriesItems()
                {
                    CategoryId = cid,
                    CategoryName = _dbContext.LKPCoverageItemCategories.Where(i => i.CategoryId == cid).First().Name,
                    CoverageItems = customerItems.Where(i => i.CategoryId == cid).OrderBy(it => it.Name)
                };
                items.Add(item);
            }

            return items;
        }

        public void AddCustomerItem(CoverageItem coverageItem)
        {
            Guid itemId = Guid.NewGuid();

            // Try to find the item
            var itemExisting = _dbContext.CoverageItems
                .Where(item => item.CategoryId == coverageItem.CategoryId
                && item.Value == coverageItem.Value
                && string.Compare(item.Name, coverageItem.Name, true) == 0)
                .FirstOrDefault();
            if (itemExisting != null) itemId = itemExisting.ItemId;
            else
            {
                // Add the new item
                _dbContext.CoverageItems.Add(new CoverageItem
                {
                    ItemId = itemId,
                    Name = coverageItem.Name,
                    Value = coverageItem.Value,
                    CategoryId = coverageItem.CategoryId
                });
            }

            // Add the customer reference data
            _dbContext.CustomerItems.Add(new CustomerItem
            {
                CustomerId = 1,
                ItemId = itemId
            });

            // Save
            _dbContext.SaveChanges();
        }

        public void RemoveCustomerItem(Guid itemId)
        {
            var item = _dbContext.CustomerItems
                .Where(i => i.CustomerId == 1 && i.ItemId == itemId)
                .FirstOrDefault();

            if (item != null)
            {
                _dbContext.CustomerItems.Remove(item);
                _dbContext.SaveChanges();
            }
        }
    }
}
