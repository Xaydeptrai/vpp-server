﻿using Microsoft.AspNetCore.Identity;
using vpp_server.Models;

namespace vpp_server.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed roles
            var roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed admin user
            var adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Name = "Nguyễn Trọng Xây",
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "0765066514"
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        public static async Task SeedCatalogsAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (!context.Catalogs.Any())
            {
                var catalogs = new[]
                {
                    new Catalog { Name = "Bìa hồ sơ" },
                    new Catalog { Name = "Bút kí" },
                    new Catalog { Name = "Bút bi" },
                    new Catalog { Name = "Sổ" },
                    new Catalog { Name = "Băng keo" },
                    new Catalog { Name = "Bảng tên, dây đeo" },
                    new Catalog { Name = "Bút chì gỗ" },
                    new Catalog { Name = "Hoá đơn" },
                    new Catalog { Name = "Giấy các loại" },
                    new Catalog { Name = "Bấm kim" },
                    new Catalog { Name = "Máy tính" },
                    new Catalog { Name = "Thước" },
                    new Catalog { Name = "Kẹp giấy" },
                    new Catalog { Name = "Bút lông" },
                    new Catalog { Name = "Kệ tài liệu" }
                };

                context.Catalogs.AddRange(catalogs);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProductsAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (!context.Products.Any())
            {
                var products = new[]
                {
                    new Product { Name = "Product 1", Price = 100, Description = "Description 1", ImageUrl = "http://example.com/image1.jpg", Stock = 10, CatalogId = 1 },
                    new Product { Name = "Product 2", Price = 200, Description = "Description 2", ImageUrl = "http://example.com/image2.jpg", Stock = 20, CatalogId = 2 },
                    new Product { Name = "Product 3", Price = 300, Description = "Description 3", ImageUrl = "http://example.com/image3.jpg", Stock = 30, CatalogId = 3 },
                    new Product { Name = "Product 4", Price = 400, Description = "Description 4", ImageUrl = "http://example.com/image4.jpg", Stock = 40, CatalogId = 4 }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
