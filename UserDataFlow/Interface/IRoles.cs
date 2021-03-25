using UserDataFlow.Model.Role;

namespace UserDataFlow.Interface
{
    public interface IRoles
    {
        RoleAndDepartmentRes GetRoleAndDepartments();
        RoleRes AddRole(RoleReq req);
        DepartmentRes AddDepartment(DepartmentReq req);
    }
}