using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_data
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal AmountBilled { get; set; }
        public DateTime? SentDate {  get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public InvoiceStatus Status { get; set; }

        public virtual ICollection<InvoicePayment>? Payments { get; set; } 
    }

    public enum InvoiceStatus
    {
        Prepared,
        Sent,
        PartiallyPaid,
        Paid,
        Delinquent,
        Refunded,
        Cancelled
    }
}
