using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity.Extensions
{
    /// <summary>
    /// IServiceCollection拓展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Module
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModule<TModule>(this IServiceCollection services) where TModule : IModule
        {

            ModuleCollection moduleCollection = new();
            // 查找模块信息
            moduleCollection.FindModule<TModule>(services);
            // 注入单例模块集合
            services.TryAddSingleton(moduleCollection);
            //模块管理者
            IModuleManager moduleManager = new ModuleManager();
            // 统一调用ModuleManager
            moduleManager.ConfigureServices(services);
            //加入单例
            services.TryAddSingleton(moduleManager);

            return services;
        }

    }
}
