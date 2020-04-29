using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbHelp;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对IService的扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 自定义配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="steup"></param>
        public static void AddMongoDBServer(this IServiceCollection services, IConfiguration configuration,Action<MongoDbConfigInfo> steup)
        {
            services.Configure(steup);
            services.Configure<MongoDbConfigInfo>(options =>
            {
                configuration.GetSection("MongoDbOptions").Bind(options);
            });
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var options = sp.GetService<IOptions<MongoDbConfigInfo>>()?.Value ?? throw new ArgumentNullException(nameof(MongoDbConfigInfo));
                return new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
            });
        }
        /// <summary>
        /// 配置1
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddMongoDBServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfigInfo>(options =>
            {
                configuration.GetSection("MongoDbOptions").Bind(options);
            });
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var options = sp.GetService<IOptions<MongoDbConfigInfo>>()?.Value ?? throw new ArgumentNullException(nameof(MongoDbConfigInfo));
                return new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
            });
        }
    }
}
