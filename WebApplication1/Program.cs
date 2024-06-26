using HttpAccess;
using Dto;
using Servicios;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped(typeof(IContextHttpLogin), provider => new RestContextLogin(
                    builder.Configuration.GetConnectionString("UrlLogin")
            ));

            builder.Services.AddScoped(typeof(IContextHttpMovimientoDeStock), provider => new RestContextMovimientoDeStock(
                    builder.Configuration.GetConnectionString("UrlMovimientoDeStock"), new HttpContextAccessor()
            ));

            builder.Services.AddScoped(typeof(IContextHttpArticulo), provider => new RestContextArticulo(
                    builder.Configuration.GetConnectionString("UrlArticulos"), new HttpContextAccessor()
            ));
            
            builder.Services.AddScoped(typeof(IContextHttpTipoDeMovimiento), provider => new RestContextTipoDeMovimiento(
                    builder.Configuration.GetConnectionString("UrlTiposMovimiento"), new HttpContextAccessor()
            ));

            builder.Services.AddScoped(typeof(IServicioMovimientoDeStock), typeof(ServicioMovimientoDeStock));
            builder.Services.AddScoped(typeof(IServicioLogin<LoginDto, LoginOutDto>), typeof(ServicioLogin));
            builder.Services.AddScoped(typeof(IServicioArticulo), typeof(ServicioArticulo));
            builder.Services.AddScoped(typeof(IServicioTipoDeMovimiento), typeof(ServicioTipoDeMovimiento));

            builder.Services.AddHttpContextAccessor();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSession();

            app.Run();
        }
    }
}
