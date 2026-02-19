namespace Enterprise.Security.Client.Core.Common
{
    public class Permissions
    {
        // Módulo de Usuarios
        public static class Users
        {
            public const string View = "users.view";
            public const string Create = "users.create";
            public const string Edit = "users.edit";
            public const string Delete = "users.delete";
        }

        // Módulo de Roles
        public static class Roles
        {
            public const string View = "roles.view";
            public const string Manage = "roles.manage";
            public const string Assign = "roles.assign";
        }

        // Módulo de Auditoría
        public static class Audits
        {
            public const string View = "audits.view";
            // Generalmente un auditor solo ve, no edita ni borra logs (por seguridad)
            // public const string Export = "audits.export"; // Podrías agregar este a futuro
        }

        // Módulo de Permisos (Para que alguien pueda asignar permisos a roles)
        public static class SystemPermissions
        {
            public const string Manage = "permissions.manage";
        }

        // --- AGREGAR ESTO ---
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class Orders
        {
            public const string ViewAll = "Permissions.Orders.ViewAll"; // Supervisor
            public const string ViewMy = "Permissions.Orders.ViewMy";   // Cliente
            public const string Create = "Permissions.Orders.Create";   // Cliente
            public const string Approve = "Permissions.Orders.Approve"; // Supervisor
        }


        // Helper para obtener todos (útil para registrar policies en bucle)
        public static List<string> GetAll()
        {
            return new List<string>
        {
            Users.View, Users.Create, Users.Edit, Users.Delete,
            Roles.View, Roles.Manage, Roles.Assign,
            Audits.View,
            Categories.View, Categories.Create, Categories.Edit, Categories.Delete,
            Products.View, Products.Create, Products.Edit, Products.Delete,
            Orders.ViewAll, Orders.ViewMy, Orders.Create, Orders.Approve
        };
        }
    }
}
