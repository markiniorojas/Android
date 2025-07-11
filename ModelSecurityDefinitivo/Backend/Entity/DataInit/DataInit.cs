using Entity.Context;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DataInit
{
    public class DataInit
    {
        private readonly ApplicationDbContext _context;

        public DataInit(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            if (!_context.Rol.Any())
            {
                var Roles = new[]
                {
                    new Rol {Name = "Administrador", Description = "Rol con todos los permisos del sistema"},
                };
                await _context.Rol.AddRangeAsync(Roles);
            }

            if (!_context.Module.Any())
            {
                var Modules = new[]
                {
                    new Module {Name = "User", Description = "Entorno de user"},
                    new Module {Name = "Module", Description = "Entorno de Module"},
                };
                await _context.Module.AddRangeAsync(Modules);
            }

            if (!_context.Permission.Any())
            {
                var permissions = new[]
                {
                    new Permission {Name = "Crear Usuario", Description = "Permiso de crear Usuarios"},
                    new Permission {Name = "Editar Usuarios", Description = "Permiso de crear Usuarios"},
                };
                await _context.Permission.AddRangeAsync(permissions);
            }

            if (!_context.Form.Any())
            {
                var Forms = new[]
                {
                    new Form {Name = "Crear User", Url = "user/create",Description = "Formulario para crear Usuarios"},
                    new Form {Name = "Editar User",  Url = "user/edit=?id",Description = "Formulario para editar Usuarios"},
                };
                await _context.Form.AddRangeAsync(Forms);
            }

            if (_context.Person.Any())
            {
                var Persons = new[]
                {
                    new Person
                    {
                        Name = "Juan",
                        LastName = "Pérez",
                        TypeDocument = "DNI",
                        DocumentNumber = "12345678",
                        Phone = "123-456-7890",
                        Address = "Calle Falsa 123",
                        IsDelete = false
                    }
                };

                await _context.Person.AddRangeAsync(Persons);
            }

            if (!_context.User.Any())
            {
                var usuers = new[]
                {
                    new User {Email = "juan@ejemplo.com",
                        Password = "juan1234_",
                        CreatedDate = DateTime.UtcNow,
                        Active = true,
                        IsDelete = false
                    },
                };

                await _context.User.AddRangeAsync(usuers);
            }

            if (!_context.RolUser.Any())
            {
                var rolUsers = new[]
                {
                    new RolUser{ RolId = 1, UserId = 1}
                };

                await _context.RolUser.AddRangeAsync(rolUsers);
            }

            if (!_context.FormModule.Any())
            {
                var formModules = new[]
                {
                    new FormModule{ FormId = 1, ModuleId = 1}
                };

                await _context.FormModule.AddRangeAsync(formModules);
            }

            if (!_context.RolFormPermission.Any())
            {
                var rolFormPermissions = new[]
                {
                    new RolFormPermission{RolId = 1 , FormId = 1, PermissionId = 1}
                };

                await _context.RolFormPermission.AddRangeAsync(rolFormPermissions);
            }
            
        }
    }
}
