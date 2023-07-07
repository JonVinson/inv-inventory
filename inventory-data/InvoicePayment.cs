using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_data
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountPaid { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
    }
}
