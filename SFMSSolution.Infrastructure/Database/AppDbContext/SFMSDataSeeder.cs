using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SFMSSolution.Infrastructure.Database.AppDbContext
{
    public static class SFMSDataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SFMSDbContext>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            // Ensure the database is created
            await context.Database.MigrateAsync();

            //// Create roles if they don't exist
            //var roles = new[] { "Admin", "Owner", "Customer" };
            //foreach (var roleName in roles)
            //{
            //    if (!await roleManager.RoleExistsAsync(roleName))
            //    {
            //        await roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
            //    }
            //}

            // Seed users
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FullName = "System Admin",
                Gender = Gender.Male,
                Status = EntityStatus.Active,
                EmailConfirmed = true
            };
            var ownerUser = new User
            {
                UserName = "owner@gmail.com",
                Email = "owner@gmail.com",
                FullName = "John Doe",
                Gender = Gender.Male,
                Status = EntityStatus.Active,
                EmailConfirmed = true
            };
            var customerUser = new User
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                FullName = "Jane Smith",
                Gender = Gender.Female,
                Status = EntityStatus.Active,
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await context.SaveChangesAsync(); // 🔥 Save user first before assigning roles
                    await userManager.AddToRoleAsync(adminUser, Role.Admin.ToString());
                }
            }

            if (await userManager.FindByEmailAsync(ownerUser.Email) == null)
            {
                var result = await userManager.CreateAsync(ownerUser, "Owner123!");
                if (result.Succeeded)
                {
                    await context.SaveChangesAsync(); // 🔥 Save user first before assigning roles
                    await userManager.AddToRoleAsync(ownerUser,Role.Owner.ToString());
                }
            }

            if (await userManager.FindByEmailAsync(customerUser.Email) == null)
            {
                var result = await userManager.CreateAsync(customerUser, "Customer123!");
                if (result.Succeeded)
                {
                    await context.SaveChangesAsync(); // 🔥 Save user first before assigning roles
                    await userManager.AddToRoleAsync(customerUser, Role.Customer.ToString());
                }
            }

            // 👇 Lưu các thay đổi vào cơ sở dữ liệu để tránh lỗi liên quan đến các khóa ngoại
            await context.SaveChangesAsync();

            // Seed Clients
            if (await applicationManager.FindByClientIdAsync("pm") is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "pm",
                    DisplayName = "Password & Authorization with Code flow client",
                    RedirectUris =
                    {
                        new Uri("https://oauth.pstmn.io/v1/callback")
                    },
                    ClientSecret = "MySuperSecretPassword",
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.ResponseTypes.Token,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "openid",
                        Permissions.Prefixes.Scope + "api"
                    }
                };

                await applicationManager.CreateAsync(descriptor);
            }

            if (await applicationManager.FindByClientIdAsync("aw") is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "aw",
                    DisplayName = "Angular Web Client",
                    ClientSecret = "AngularSecret",
                    RedirectUris =
                    {
                        new Uri("http://localhost:5051/index.html"),
                        new Uri("http://localhost:5051/signin-callback.html"),
                        new Uri("http://localhost:5051/signin-silent-callback.html"),
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "openid",
                        Permissions.Prefixes.Scope + "api"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                };

                await applicationManager.CreateAsync(descriptor);
            }

            // Seed Scopes
            if (await scopeManager.FindByNameAsync("api") is null)
            {
                await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "API Access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("vi-VN")] = "Truy cập API"
                    },
                    Name = "api",
                    Resources =
                    {
                        "resource_a"
                    }
                });
            }
        }
    }

}
