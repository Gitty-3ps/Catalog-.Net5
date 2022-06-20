using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Catalog.Controllers
{
    // GET /items

    [ApiController] 
    [Route("items")]
    
    public class ItemsController : ControllerBase
    {

    public readonly InMemItemsRepository repository;

    public ItemsController()
    {
        repository = new InMemItemsRepository();
    }

    // Get /items
    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
        var items = repository.GetItems();
        return items;

    }
}
    
   

}