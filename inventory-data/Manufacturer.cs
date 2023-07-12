using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
