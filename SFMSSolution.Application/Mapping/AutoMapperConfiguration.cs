using Microsoft.Extensions.DependencyInjection;
using SFMSSolution.Application.Mapping.MappingProfiles;
using SFMSSolution.Application.Services.Admin;
using SFMSSolution.Application.Services.Auth;
using SFMSSolution.Application.Services.Bookings;
using SFMSSolution.Application.Services.Facilities;
using SFMSSolution.Application.Services.UserProfile;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.Repositories;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Application.Services.Events;
using SFMSSolution.Application.ExternalService.Email;
using SFMSSolution.Application.ExternalService.CDN;
using SFMSSolution.Application.Services.FacilityPrices;
using SFMSSolution.Application.Services.Reports;

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
                cfg.AddProfile<EventProfile>();
                cfg.AddProfile<FacilityPriceProfile>();
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Đăng ký Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IFacilityPriceService, FacilityPriceService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }

    }
}
