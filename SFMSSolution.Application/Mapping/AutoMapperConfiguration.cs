using Microsoft.Extensions.DependencyInjection;
using SFMSSolution.Application.Mapping.MappingProfiles;
using SFMSSolution.Application.Services.Admin;
using SFMSSolution.Application.Services.Auth;
using SFMSSolution.Application.Services.Bookings;
using SFMSSolution.Application.Services.Email;
using SFMSSolution.Application.Services.Facilities;
using SFMSSolution.Application.Services.Email;
using SFMSSolution.Application.Services.UserProfile;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.Repositories;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

namespace SFMSSolution.Application.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Đăng ký AutoMapper

            // Đăng ký AutoMapper Profiles
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthProfile>();
                cfg.AddProfile<AdminProfile>();
                cfg.AddProfile<UserProfileProfile>();
                cfg.AddProfile<FacilityProfile>();
                cfg.AddProfile<BookingProfile>();
            });
            //var config = new MapperConfiguration(cfg =>
            //{
            //    // Tự động scan tất cả Profile trong assembly này
            //    cfg.AddMaps(Assembly.GetExecutingAssembly());
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Đăng ký Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IBookingService, BookingService>();


            return services;
        }

        //public static IMapper InitializeAutoMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        // Tự động scan tất cả Profile trong assembly này
        //        cfg.AddMaps(Assembly.GetExecutingAssembly());
        //    });

        //    return config.CreateMapper();
        //}
    }
}
