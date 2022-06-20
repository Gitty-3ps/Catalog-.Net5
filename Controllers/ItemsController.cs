using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Controllers
{
    // GET /items

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        public readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // Get /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
    }



}