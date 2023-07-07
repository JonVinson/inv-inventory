using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class Employee
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime HireDate { get; set; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
}