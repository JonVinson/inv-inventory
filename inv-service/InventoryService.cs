using inventory_data;
using System.Diagnostics;

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
            return _context.Suppliers;
        }

        public int CreateSupplier(Supplier model)
        {
            _context.Suppliers.Add(model);
            _context.SaveChanges();

            return model.Id;
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
            return _context.Customers;
        }

        public int CreateCustomer(Customer model)
        {
            _context.Customers.Add(model);
            _context.SaveChanges();

            return model.Id;
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
    }
}