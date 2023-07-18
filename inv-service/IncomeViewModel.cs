using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_service
{
    public class IncomeViewModel
    {
        public string? Department { get; set; }
        public double Revenue { get; set; }
        public double Cost { get; set; }
        public double NetIncome { get; set; }
    }
}
