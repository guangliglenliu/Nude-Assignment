using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NudeAssignment.Entities;
using NudeAssignment.Models;
using NudeAssignment.Services;

namespace NudeAssignment.Controllers
{
    [Route("api/[controller]")]
    public class CustomerItemController : Controller
    {
        ICoverageItemService _service = null;

        public CustomerItemController(ICoverageItemService service)
        {
            this._service = service;
        }

        [HttpGet("[action]")]
        public IEnumerable<CustomerCategoriesItems> Get(int customerId)
        {
            var customerItems = _service.GetCustomerItems(customerId);
            return customerItems;
        }

        [HttpGet("[action]")]
        public IEnumerable<LKPCoverageItemCategory> Categories()
        {
            var categories = _service.GetCoverageItemCategories();
            return categories;
        }

        [HttpPost("[action]")]
        public void Add([FromBody]CoverageItem item)
        {
            _service.AddCustomerItem(item);
        }

        [HttpGet("[action]")]
        public void Remove(Guid itemId)
        {
            _service.RemoveCustomerItem(itemId);
        }
    }
}
