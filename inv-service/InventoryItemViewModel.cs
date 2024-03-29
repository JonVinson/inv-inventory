﻿using inventory_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_service
{
    public class InventoryItemViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public DateTime AsOfDate { get; set; }
    }
}
