using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PRN211GroupProject.Pages;

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
            builder.Services.AddTransient<LayoutModel>();
            builder.Services.AddDistributedMemoryCache(); 
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
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
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}