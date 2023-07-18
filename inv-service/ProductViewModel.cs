using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using inventory_data;

namespace inventory_service
{
    public class ProductViewModel : Product, IComparable<ProductViewModel>
    {
        public string? DepartmentCode { get; set; }
        public string? ManufacturerCode { get; set; }

        public int CompareTo(ProductViewModel? other)
        {
            return Id.CompareTo(other?.Id);
        }
    }
}
