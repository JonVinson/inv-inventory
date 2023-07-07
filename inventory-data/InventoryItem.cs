using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class InventoryItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime AsOfDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
