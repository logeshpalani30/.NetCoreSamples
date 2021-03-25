using System.Collections.Generic;

namespace UserDataFlow.Model.Role
{
    public class RoleReq
    {
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public int DepartmentId { get; set; }
    }

    public class RoleRes : RoleReq
    {
        public int RoleId { get; set; }
    }

    public class DepartmentReq
    {
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
    }

    public class DepartmentRes : DepartmentReq
    {
        public int DepartmentId { get; set; }
    }

    public class RoleAndDepartmentRes
    {
        public IEnumerable<RoleRes> Roles { get; set; }
        public IEnumerable<DepartmentRes> Department { get; set; }
    }
}