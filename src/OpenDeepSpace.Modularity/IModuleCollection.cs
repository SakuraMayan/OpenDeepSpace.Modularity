using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 模块收集
    /// </summary>
    public interface IModuleCollection
    {

        /// <summary>
        /// 构建模块依赖集
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <returns></returns>
        public List<ModuleInfo> BuildModuleDependCollection(Type moduleType);

        /// <summary>
        /// 模块排序
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public List<ModuleInfo> ModuleSort<TModule>() where TModule : IModule;

        /// <summary>
        /// 模块查找
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        public void FindModule<TModule>(IServiceCollection services) where TModule : IModule;
    }
}
