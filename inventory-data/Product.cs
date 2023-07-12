using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class Product
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int ManufacturerId { get; set; }

    public string? ModelNumber { get; set; }

    public string Description { get; set; } = null!;

    public double? Price { get; set; }

    public byte[]? Image { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<InventoryItem>? InventoryItems { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Transaction>? Transactions { get; set; }
}
