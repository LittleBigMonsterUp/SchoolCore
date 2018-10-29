using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolCore.EntityFramework;
using SchoolCore.EntityFramework.Data;

namespace SchoolCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //建立管道，注入进来(与以往不同)
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SchoolDbContext>();

                    DbInitializer.Initalizer(context);
                }
                catch (Exception e)
                {

                    //初始化系统检测试数据报错，请联系管理员。

                    //记录日志
                    var logger = services.GetRequiredService<ILogger<Program>>();
                   logger.LogError(e, "初始化系统测试数据的时候报错，请联系管理员。");

                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
