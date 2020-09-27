using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueStore.Repository.Interfaces;
using VueStore.Repository.Models;

namespace VueStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaxItemsController : ControllerBase
    {
        private readonly IItemRepo _itemRepo;

        public MaxItemsController(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }
        [HttpGet]
        public ActionResult<List<GroupedItem>> GetMaxPriceItems()
        {
            return Ok(_itemRepo.GetMaxPriceItems().Result);
        }
        [HttpGet("{name}")]
        public ActionResult<List<GroupedItem>> GetMaxPriceItem(string name)
        {
            return Ok(_itemRepo.GetMaxPriceItem(name).Result);
        }
    }
}
