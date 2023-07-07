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
    }
}