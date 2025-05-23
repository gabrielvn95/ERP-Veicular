using GestVeicular.Repositorio;
using GestVeicular.Services.ClienteService;
using GestVeicular.Services.SenhaService;
using GestVeicular.Services.ServicosService;
using GestVeicular.Services.SessaoService;
using GestVeicular.Services.VeiculoService;
using GestVeicular.Services.VendaService;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISessaoInterface, SessaoService>();
            builder.Services.AddScoped<IVeiculoInterface, VeiculoService>();
            builder.Services.AddScoped<IServicosInterface, ServicosService>();
            builder.Services.AddScoped<ISenhaInterface, SenhaService>();
            builder.Services.AddScoped<IVendaInterface, VendaService>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IClienteInterface, ClienteService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
