using inventory_data;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace inventory_service
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
        public IQueryable<Company> GetSuppliers()
        {
            return _context.Companies
                .Where(c => c.CompanyType == CompanyType.Supplier)
                .OrderBy(s => s.Name);
        }

        public int CreateSupplier(Company model)
        {
            var supplier = new Company
            {
                CompanyType = CompanyType.Supplier,
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

            _context.Companies.Add(supplier);
            _context.SaveChanges();

            return supplier.Id;
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Companies.FirstOrDefault(p => p.Id == id);

            if (supplier != null)
            {
                _context.Companies.Remove(supplier);
            }

            _context.SaveChanges();
        }

        public void UpdateSupplier(Company model)
        {
            var supplier = _context.Companies.FirstOrDefault(p => p.Id == model.Id);

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
        public IQueryable<Company> GetCompanies()
        {
            return _context.Companies
                .OrderBy(c => c.Name);
        }

        public IQueryable<Company> GetCustomers()
        {
            return _context.Companies
                .Where(c => c.CompanyType == CompanyType.Customer)
                .OrderBy(c => c.Name);
        }

        public int CreateCustomer(Company model)
        {
            var customer = new Company
            {
                CompanyType = CompanyType.Customer,
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

            _context.Companies.Add(customer);
            _context.SaveChanges();

            return customer.Id;
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Companies.FirstOrDefault(p => p.Id == id);

            if (customer != null)
            {
                _context.Companies.Remove(customer);
            }

            _context.SaveChanges();
        }

        public void UpdateCustomer(Company model)
        {
            var customer = _context.Companies.FirstOrDefault(p => p.Id == model.Id);

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
        public IQueryable<ProductViewModel> GetProducts(int? deptId = null, int? manuId = null, string? name = null)
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

            if ((deptId ?? 0) != 0)
            {
                query = query.Where(p => p.DepartmentId == deptId);
            }

            if ((manuId ?? 0) != 0)
            {
                query = query.Where(p => p.ManufacturerId == manuId);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Description.Contains(name) || p.ModelNumber.Contains(name));
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

        #region InventoryItems
        public IQueryable<InventoryItemViewModel> GetInventoryItems(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.PhysicalInventory as IQueryable<InventoryItem>;

            if (startDate != null)
            {
                query = query.Where(p => p.AsOfDate >= startDate);
            }

            if (endDate != null)
            {
                query = query.Where(p => p.AsOfDate <= endDate);
            }

            return query.Select(p => new InventoryItemViewModel
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Description = p.Product.Department.Code + " - " + p.Product.Manufacturer.Code
                    + " - " + p.Product.ModelNumber + " - " + p.Product.Description,
                Quantity = p.Quantity,
                AsOfDate = p.AsOfDate
            })
            .OrderBy(p => p.AsOfDate);
        }

        public int CreateInventoryItem(InventoryItemViewModel model)
        {
            var inventoryItem = new InventoryItem
            {
                ProductId = model.ProductId,
                AsOfDate = model.AsOfDate,
                Quantity = model.Quantity,
            };

            _context.PhysicalInventory.Add(inventoryItem);
            _context.SaveChanges();

            return inventoryItem.Id;
        }

        public void DeleteInventoryItem(int id)
        {
            var inventoryItem = _context.PhysicalInventory.Find(id);

            if (inventoryItem != null)
            {
                _context.PhysicalInventory.Remove(inventoryItem);
            }

            _context.SaveChanges();
        }

        public void UpdateInventoryItem(InventoryItemViewModel model)
        {
            var inventoryItem = _context.PhysicalInventory.Find(model.Id);

            if (inventoryItem != null)
            {
                inventoryItem.ProductId = model.ProductId;
                inventoryItem.AsOfDate = model.AsOfDate;
                inventoryItem.Quantity = model.Quantity;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Transactions
        public IQueryable<TransactionViewModel> GetTransactions(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Transactions
                .Select(p => new TransactionViewModel
                {
                    Id = p.Id,
                    Date = p.Date,
                    TransactionType = p.TransactionType,
                    ProductId = p.ProductId,
                    ProductName = p.Product.Department.Code + " - " + p.Product.Manufacturer.Code
                        + " - " + p.Product.ModelNumber + " - " + p.Product.Description,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company == null ? null : p.Company.Code + " - " + p.Company.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Note = p.Note
                });

            if (startDate != null)
            {
                query = query.Where(p => p.Date >= startDate);
            }

            if (endDate != null)
            {
                query = query.Where(p => p.Date <= endDate);
            }

            return query.OrderBy(p => p.Date);
        }

        public int CreateTransaction(TransactionViewModel model)
        {
            var transaction = new Transaction
            {
                Date = model.Date,
                TransactionType = model.TransactionType,
                ProductId = model.ProductId,
                CompanyId = model.CompanyId,
                Quantity = model.Quantity,
                Price = model.Price.GetValueOrDefault(),
                Note = model.Note
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.Id;
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            _context.SaveChanges();
        }

        public void UpdateTransaction(TransactionViewModel model)
        {
            var transaction = _context.Transactions.Find(model.Id);

            if (transaction != null)
            {
                transaction.Date = model.Date;
                transaction.TransactionType = model.TransactionType;
                transaction.ProductId = model.ProductId;
                transaction.CompanyId = model.CompanyId;
                transaction.Quantity = model.Quantity;
                transaction.Price = model.Price.GetValueOrDefault();
                transaction.Note = model.Note;
                _context.SaveChanges();
            }
        }
        #endregion

        #region Reports

        public IEnumerable<IncomeViewModel> IncomeReport(DateTime? startDate, DateTime? endDate)
        {
            var purchases = _context.Transactions.Where(t => t.TransactionType == TransactionType.Purchase);
            var sales = _context.Transactions.Where(t => t.TransactionType == TransactionType.Sale);

            if (startDate != null)
            {
                purchases = purchases.Where(t => t.Date >= startDate);
                sales = sales.Where(t => t.Date >= startDate);
            }

            if (endDate != null)
            {
                purchases = purchases.Where(t => t.Date <= endDate);
                sales = sales.Where(t => t.Date <= endDate);
            }

            var totals = _context.Departments.ToDictionary(d => d.Name!, d => new IncomeViewModel { Department = d.Name });

            var totalPurchases = purchases.GroupBy(t => t.Product.Department.Name,
                (k, g) => new { Department = k, Total = g.Sum(t => t.Price * t.Quantity) });

            foreach (var item in totalPurchases)
            {
                totals[item.Department!].Cost = item.Total;
            }

            var totalSales = sales.GroupBy(t => t.Product.Department.Name,
                (k, g) => new { Department = k, Total = g.Sum(t => t.Price * t.Quantity) });

            foreach (var item in totalSales)
            {
                totals[item.Department!].Revenue = item.Total;
            }

            var totalIncome = totals.Values
                .OrderBy(i => i.Department)
                .ToList();

            var grandTotalPurchases = purchases.Sum(t => t.Quantity * t.Price);
            var grandTotalSales = sales.Sum(t => t.Quantity * t.Price);

            totalIncome.Add(new IncomeViewModel
            {
                Department = "Total",
                Revenue = grandTotalSales,
                Cost = grandTotalPurchases,
                NetIncome = grandTotalSales - grandTotalPurchases
            });

            return totalIncome;
        }

        #endregion
    }
}