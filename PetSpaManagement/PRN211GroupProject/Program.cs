using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService;

namespace PRN211GroupProject
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();
        }
        


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
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
            builder.Services.AddDistributedMemoryCache(); // Required for session state
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Make the session cookie HttpOnly
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            builder.Services.AddDbContext<PetSpaManagementContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("PetSpaManagement")));

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}