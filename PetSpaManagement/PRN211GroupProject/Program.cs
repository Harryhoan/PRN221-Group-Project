using Microsoft.AspNetCore.Authentication.Cookies;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.FeedbacksService;
using PetSpaService.RolesService;
using PetSpaService.ServicesService;
using PetSpaService.AvailableService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using PRN211GroupProject.Pages;
using PetSpaService.BillService;
using PetSpaService.BillDetailedService;

namespace PRN211GroupProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAvailableService, AvailableService>();
            builder.Services.AddScoped<IBillDetailedService, BillDetailedService>();
            builder.Services.AddScoped<IBillService, BillService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<ISpotService, SpotService>();
            builder.Services.AddScoped<IVoucherService, VoucherService>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
            });
            builder.Services.AddMvc();
            builder.Services.AddTransient<LayoutModel>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login"; // Specify your login path

        });


            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // or MapRazorPages(), depending on your setup
            });

            app.Run();
        }
    }
}