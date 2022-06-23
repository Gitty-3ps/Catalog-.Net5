using System;
namespace Catalog.Api.Entities
{
    public class Item
    {

        public Guid Id { get; set; } // init similar to set, but you cannot modify after creation

        public string Name { get; set; }

        public string Description {get; set;}

        public decimal Price {get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}