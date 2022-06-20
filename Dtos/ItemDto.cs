using System;

namespace Catalog.Dtos
{
    public record ItemDto
    {
        public Guid Id { get; init; } // init similar to set, but you cannot modify after creation
        public string Name { get; init; }
        public decimal Price {get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }

}