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

    public int? CompanyId { get; set; }

    public DateTime Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public string? Note { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Product Product { get; set; } = null!;
}

public enum TransactionType
{
    Purchase,
    Sale,
    PurchaseReturn,
    SaleReturn,
    Adjustment
}
