using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.DepartmentDto
{
    public class GetDepartmentDto
    {
        public int department_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
