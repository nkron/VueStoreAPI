using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VueStore.Repository.Interfaces;
using VueStore.Repository.Models;

namespace VueStoreAPI.Controllers
{
    //api/Items
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IItemRepo _itemRepo;

        public ItemsController(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }


        // Get api/items/{id}
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<Item> GetAll()
        {
            var item = _itemRepo.GetAll().Result;

            return Ok(item);
        }
        [HttpGet("{id}")]
        public ActionResult<Item> GetByID(int id)
        {
            return Ok(_itemRepo.GetItemByID(id).Result);
        }

        //Post api/items
        [HttpPost]
        public ActionResult<Item> Create(Item item)
        {
            var result = _itemRepo.CreateItem(item).Result;

            return result != 0 ? Ok(result) : throw new Exception("No record created in database");
        }

        //Patch api/items
        [HttpPatch]
        public ActionResult<int> Update(Item item)
        {
            var result = _itemRepo.UpdateItem(item).Result;

            return Ok(result);
        }

        //Delete api/items
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            var rowsDeleted = _itemRepo.DeleteItem(id).Result;

            return Ok(rowsDeleted);
        }
    }
}
