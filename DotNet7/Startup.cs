//using DotNet7.Config;
//using DotNet7.Data;
//using DotNet7.Provider;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace DotNet7
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.Configure<DatabaseConfig>(Configuration.GetSection("ConnectionStrings"));
//            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
//            services.AddScoped<ProductProvider>();
            

//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            // Sử dụng middleware và cấu hình khác
//            app.UseCors("AllowAll");

//            app.UseRouting();

//            // Thêm tất cả các middleware bạn muốn chạy trước khi xử lý endpoints
//            // ...

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
