using DotNetCore.Cap.Demo.Publisher.PgModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCore.Cap.Demo.Publisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<PgDbContext>(p => p.UseNpgsql("Server=localhost;Port=5432;UserId=jiangyi;Password=jiangyi;Database=cap_test;"));

            services.AddCap(x =>
            {
                //rabbitmq在docker运行时，需要映射两个端口：5672和15672
                //5672供程序集访问
                //15672供web访问
                //默认用户名和密码为：guest/guest
                x.UseRabbitMQ(p =>
                {
                    p.HostName = "localhost";
                    p.Port = 5672;
                    p.UserName = "guest";
                    p.Password = "guest";
                });
                //mysql在docker运行时，需要映射端口：3306
                //x.UseMySql("Server = localhost; Port = 8089; Database = captest; Uid = root; Pwd = my-secret-pw; ");

                //pg
                //x.UsePostgreSql("Server=localhost;Port=5432;UserId=jiangyi;Password=jiangyi;Database=cap_test;"");
                x.UseEntityFramework<PgDbContext>();
                //x.FailedRetryCount = 1;

                //x.UseDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
