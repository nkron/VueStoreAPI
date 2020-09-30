using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VueStore.Repository.Interfaces;
using VueStore.Repository.Models;
using VueStoreAPI.Core;
using Microsoft.AspNetCore.Http;

namespace VueStoreAPI.Controllers
{
    //api/Items
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IItemRepo _itemRepo;
        private readonly IFakeLogger _logger;

        public ItemsController(IItemRepo itemRepo, IFakeLogger logger)
        {
            _itemRepo = itemRepo;
            _logger = logger;
        }


        // Get api/items/{id}
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            var items = new List<Item>();
            try
            {

                items = _itemRepo.GetAll().Result.ToList();
            }
            catch (Exception e)
            {
                //Hide Stacktrace from users
                _logger.log(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(items);
        }
        [HttpGet("{id}")]
        public ActionResult<Item> GetByID(int id)
        {
            return Ok(_itemRepo.GetItemByID(id).Result);
        }

        //Post api/items
        [HttpPost]
        public ActionResult<int> Create(string itemName, int cost)
        {
            var item = new Item { ItemName = itemName, Cost = cost };
            item.Id = _itemRepo.CreateItem(item).Result;

            return Created("URI goes here", item);
        }

        //Patch api/items
        [HttpPatch]
        public ActionResult<int> Update(Item item)
        {
            var result = _itemRepo.UpdateItem(item).Result;

            return NoContent();
        }

        //Delete api/items
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            var rowsDeleted = _itemRepo.DeleteItem(id).Result;

            return NoContent();
        }
    }
}
