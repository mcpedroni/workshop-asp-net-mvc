using System.Linq;
using SalesWebMVC.Models;
using System.Collections.Generic;

namespace SalesWebMVC.Services {

    public class DepartmentService {

        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context) {
            _context = context;
        }

        public List<Department> FindAll() {
            return _context.Department.OrderBy(d => d.Name).ToList();
        }
    }
}
