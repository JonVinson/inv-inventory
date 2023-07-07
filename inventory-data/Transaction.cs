using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public int? EmployeeId { get; set; }

    public int? CustomerId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime Date { get; set; }

    public int TransactionType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier? Supplier { get; set; }
}
