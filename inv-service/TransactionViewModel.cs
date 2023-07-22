using inventory_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_service
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? Total { get { return Quantity * Price; } }
        public string? Note { get; set; }
    }
}
