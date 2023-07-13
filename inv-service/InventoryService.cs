using inventory_data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace inv_service
{
    public class InventoryService
    {
        private readonly InventoryContext _context;

        public InventoryService(InventoryContext context)
        {
            _context = context;
        }

        #region Departments
        public IQueryable<Department> GetDepartments()
        {
            return _context.Departments;
        }

        public int CreateDepartment(string name, string code)
        {
            var department = new Department
            {
                Name = name,
                Code = code
            };

            _context.Departments.Add(department);
            _context.SaveChanges();

            return department.Id;
        }

        public void DeleteDepartment(int id)
        {
            var department = _context.Departments.FirstOrDefault(p => p.Id == id);

            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            _context.SaveChanges();
        }

        public void UpdateDepartment(int id, string name, string code)
        {
            var department = _context.Departments.FirstOrDefault(p => p.Id == id);

            if (department != null)
            {
                if (name != null) department.Name = name;
                if (code != null) department.Code = code;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Suppliers
        public IQueryable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.OrderBy(s => s.Name);
        }

        public int CreateSupplier(Supplier model)
        {
            var supplier = new Supplier
            {
                Name = model.Name,
                Code = model.Code,
                Street = model.Street,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode,
                ContactEmail = model.ContactEmail,
                ContactName = model.ContactName,
                PhoneNumber = model.PhoneNumber
            };

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return supplier.Id;
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(p => p.Id == id);

            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }

            _context.SaveChanges();
        }

        public void UpdateSupplier(Supplier model)
        {
            var supplier = _context.Suppliers.FirstOrDefault(p => p.Id == model.Id);

            if (supplier != null)
            {
                supplier.Name = model.Name;
                supplier.Code = model.Code;
                supplier.Street = model.Street;
                supplier.City = model.City;
                supplier.State = model.State;
                supplier.Country = model.Country;
                supplier.PostalCode = model.PostalCode;
                supplier.ContactEmail = model.ContactEmail;
                supplier.ContactName = model.ContactName;
                supplier.PhoneNumber = model.PhoneNumber;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Customers
        public IQueryable<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.Name);
        }

        public int CreateCustomer(Customer model)
        {
            var customer = new Customer
            {
                Name = model.Name,
                Code = model.Code,
                Street = model.Street,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode,
                ContactEmail = model.ContactEmail,
                ContactName = model.ContactName,
                PhoneNumber = model.PhoneNumber
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.Id;
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(p => p.Id == id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer model)
        {
            var customer = _context.Customers.FirstOrDefault(p => p.Id == model.Id);

            if (customer != null)
            {
                customer.Name = model.Name;
                customer.Code = model.Code;
                customer.Street = model.Street;
                customer.City = model.City;
                customer.State = model.State;
                customer.Country = model.Country;
                customer.PostalCode = model.PostalCode;
                customer.ContactEmail = model.ContactEmail;
                customer.ContactName = model.ContactName;
                customer.PhoneNumber = model.PhoneNumber;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Manufacturers
        public IQueryable<Manufacturer> GetManufacturers()
        {
            return _context.Manufacturers.OrderBy(c => c.Name);
        }

        public int CreateManufacturer(Manufacturer model)
        {
            var manufacturer = new Manufacturer
            {
                Name = model.Name,
                Code = model.Code,
            };

            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            return manufacturer.Id;
        }

        public void DeleteManufacturer(int id)
        {
            var manufacturer = _context.Manufacturers.FirstOrDefault(p => p.Id == id);

            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
            }

            _context.SaveChanges();
        }

        public void UpdateManufacturer(Manufacturer model)
        {
            var manufacturer = _context.Manufacturers.FirstOrDefault(p => p.Id == model.Id);

            if (manufacturer != null)
            {
                manufacturer.Name = model.Name;
                manufacturer.Code = model.Code;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Products
        public IQueryable<ProductViewModel> GetProducts(string? filter = null)
        {
            var query = _context.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    DepartmentId = p.DepartmentId,
                    ManufacturerId = p.ManufacturerId,
                    DepartmentCode = p.Department.Code,
                    ManufacturerCode = p.Manufacturer.Code,
                    ModelNumber = p.ModelNumber,
                    Description = p.Description,
                    Price = p.Price
                });

            if (filter != null)
            {
                query = query.Where(p => p.Description.Contains(filter));
            }

            return query.OrderBy(p => p.DepartmentCode)
                .ThenBy(p => p.ManufacturerCode)
                .ThenBy(p => p.Description);
        }

        public int CreateProduct(ProductViewModel model)
        {
            var product = new Product
            {
                DepartmentId = model.DepartmentId,
                ManufacturerId = model.ManufacturerId,
                ModelNumber = model.ModelNumber,
                Description = model.Description,
                Price = model.Price
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product.Id;
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
            }

            _context.SaveChanges();
        }

        public void UpdateProduct(ProductViewModel model)
        {
            var product = _context.Products.Find(model.Id);

            if (product != null)
            {
                product.Description = model.Description;
                product.Price = model.Price;
                product.ManufacturerId = model.ManufacturerId;
                product.ModelNumber = model.ModelNumber;
                product.DepartmentId = model.DepartmentId;
                _context.SaveChanges();
            }
        }
        #endregion
    }
}