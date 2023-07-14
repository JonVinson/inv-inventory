using System;
using System.Collections.Generic;

namespace inventory_data;

public partial class Company
{
    public int Id { get; set; }
    public CompanyType CompanyType { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set;}

    public virtual ICollection<Transaction>? Transactions { get; set; }
}

public enum CompanyType
{
    Other,
    Supplier,
    Customer
}
