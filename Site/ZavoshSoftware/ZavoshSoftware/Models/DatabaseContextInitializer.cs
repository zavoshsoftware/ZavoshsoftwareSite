using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal static class DatabaseContextInitializer
    {
        static DatabaseContextInitializer()
        {

        }

        internal static void Seed(DatabaseContext databaseContext)
        {
            Guid roleId = Guid.NewGuid();

            InsertBaseRole(databaseContext, roleId);
            InsertBaseUser(databaseContext, roleId);

            InsertBasePageGroup(databaseContext, new Guid("30fa953c-403f-4796-b787-528238a48100"), "خدمات", "خدمات");
            InsertBasePageGroup(databaseContext, new Guid("E3F46BE1-1F0A-411C-A540-A5A25F971B01"), "نمونه کارها", "نمونه کارها");
            InsertBasePageGroup(databaseContext, new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E"), "وبلاگ", "وبلاگ");

            InsertBasePosition(databaseContext, new Guid("a06ba1f6-69f3-424b-b629-69ac5eec99a8"), "منو خدمات");
            InsertBasePosition(databaseContext, new Guid("edeb818d-e965-4def-b855-17e04f40f1a6"), "خدمات اصلی صفحه اصلی");
            InsertBasePosition(databaseContext, new Guid("28b4c30b-f5e4-4c82-a67e-9109e9f28cd1"), "خدمات جزیی صفحه اصلی");


            databaseContext.SaveChanges();
        }

        internal static void InsertBaseRole(DatabaseContext databaseContext, Guid roleId)
        {
            Role role = new Role()
            {
                Id = roleId,
                Title = "مدیر وب سایت",
                Name = "Administrator",
                CreationDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.Roles.Add(role);
        }

        internal static void InsertBaseUser(DatabaseContext databaseContext, Guid roleId)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = roleId,
                Username = "zavosh",
                Password = "123456",
                FirstName = "baseuser",
                LastName = "baseuser",
                CreationDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.Users.Add(user);
        }

        internal static void InsertBasePageGroup(DatabaseContext databaseContext, Guid id, string urlParam, string title)
        {
            PageGroup pageGroup = new PageGroup()
            {
                Id = id,
                Title = title,
                LastModificationDate = DateTime.Now,
                UrlParameter = urlParam,
                Body = "seed",
                CreationDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.PageGroups.Add(pageGroup);
        }


        internal static void InsertBasePosition(DatabaseContext databaseContext, Guid id, string title)
        {
            Position position = new Position()
            {
                Id = id,
                Title = title,
                LastModificationDate = DateTime.Now,
                CreationDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.Positions.Add(position);
        }
    }
}

