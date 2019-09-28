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

            foreach (var category in _dbContext.LKPCoverageItemCategories)
            {
                var item = new CustomerCategoriesItems()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.Name,
                    CoverageItems = (from customerItem in _dbContext.CustomerItems
                                     join coverageItem in _dbContext.CoverageItems on customerItem.ItemId equals coverageItem.ItemId
                                     where customerItem.CustomerId == customerId && coverageItem.CategoryId == category.CategoryId
                                     orderby coverageItem.Name
                                     select coverageItem)
                                     
                };
                // Take it if this customer have the items in this category
                if (item.CoverageItems.Any()) items.Add(item);
            }

            // Order by category name
            if (items.Any()) items = items.OrderBy(item => item.CategoryName).ToList();

            return items;
        }

        public string AddCustomerItem(CoverageItem coverageItem)
        {
            Guid itemId = Guid.NewGuid();
            bool bChanged = false;

            try
            {
                // Try to find the item
                var itemExisting = _dbContext.CoverageItems
                    .Where(item => item.CategoryId == coverageItem.CategoryId
                        && item.Value == coverageItem.Value
                        && string.Compare(item.Name, coverageItem.Name, true) == 0)
                    .FirstOrDefault();

                if (itemExisting != null) itemId = itemExisting.ItemId;
                else
                {
                    bChanged = true;
                    // Add the new item
                    _dbContext.CoverageItems.Add(new CoverageItem
                    {
                        ItemId = itemId,
                        Name = coverageItem.Name,
                        Value = coverageItem.Value,
                        CategoryId = coverageItem.CategoryId
                    });
                }

                // Duplicate check
                if (!_dbContext.CustomerItems.Any(ci => ci.ItemId == itemId))
                {
                    bChanged = true;
                    // Add the customer reference data
                    _dbContext.CustomerItems.Add(new CustomerItem
                    {
                        CustomerId = 1,
                        ItemId = itemId
                    });
                }

                // Save if changed
                if (bChanged) _dbContext.SaveChanges();
                else return null;
            }
            catch
            {
                return null;
            }

            return itemId.ToString();
        }

        /// <summary>
        /// Return 0 = removed, 1 = not found, -1 = error, controller based on this return to handle the response code (later)
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int RemoveCustomerItem(Guid itemId)
        {
            try
            {
                var item = _dbContext.CustomerItems
                    .Where(i => i.CustomerId == 1 && i.ItemId == itemId)
                    .FirstOrDefault();

                if (item == null) return 1;
                else
                {
                    _dbContext.CustomerItems.Remove(item);
                    _dbContext.SaveChanges();

                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
