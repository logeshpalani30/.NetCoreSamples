using System;
using System.Linq;
using UserDataFlow.Interface;
using UserDataFlow.Model.Role;
using UserDataFlow.Models;

namespace UserDataFlow.Repository
{
    public class RolesRepository : IRoles
    {
        private readonly logesh_user_task_dbContext _dbContext;

        public RolesRepository(logesh_user_task_dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DepartmentRes AddDepartment(DepartmentReq req)
        {
            if (string.IsNullOrEmpty(req.DepartmentName.Trim()))
            {
                throw new Exception("Department Name should not be empty");
            }
            var department = new Department()
            {
                DepartName = req.DepartmentName,
                DepartDesc = req.DepartmentDesc
            };
            var addedDepartment = _dbContext.Department.Add(department);
            
            _dbContext.SaveChanges();

            return new DepartmentRes()
            {
                DepartmentId = addedDepartment.Entity.DepartmentId,
                DepartmentDesc = req.DepartmentDesc,
                DepartmentName = req.DepartmentName
            };
        }
        public RoleRes AddRole(RoleReq req)
        {
            if (_dbContext.Department.Any(c=>c.DepartmentId == req.DepartmentId))
            {
                if (string.IsNullOrEmpty(req.RoleName.Trim()))
                {
                    throw new Exception("Role Name should not be empty");
                }

                var role = new Role()
                {
                    RoleName = req.RoleName,
                    RoleDesc = req.RoleDesc,
                    DepartmentId = req.DepartmentId
                };

                var addedRole = _dbContext.Role.Add(role);
                _dbContext.SaveChanges();

                return new RoleRes()
                {
                    DepartmentId = req.DepartmentId,
                    RoleName = req.RoleName,
                    RoleDesc = req.RoleDesc,
                    RoleId = addedRole.Entity.RoleId
                };
            }

            throw new Exception("Department not exist");
        }
        public RoleAndDepartmentRes GetRoleAndDepartments()
        {
            var roles = from role in _dbContext.Role
                                        select new RoleRes()
                                        {
                                            DepartmentId = role.DepartmentId,
                                            RoleName = role.RoleName,
                                            RoleDesc = role.RoleDesc,
                                            RoleId = role.RoleId
                                        };
            var departments = from department in _dbContext.Department
                                                select new DepartmentRes()
                                                {
                                                    DepartmentDesc = department.DepartDesc,
                                                    DepartmentId = department.DepartmentId,
                                                    DepartmentName = department.DepartName
                                                };
            if (!departments.Any())
                throw new Exception("Roles and departments not yet added");
           
            return new RoleAndDepartmentRes()
            {
                Department = departments,
                Roles = roles
            };
        }
    }
}