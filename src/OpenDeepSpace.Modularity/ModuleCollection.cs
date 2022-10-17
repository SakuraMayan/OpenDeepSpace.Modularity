using Microsoft.Extensions.DependencyInjection;
using OpenDeepSpace.Modularity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 模块集
    /// </summary>
    public class ModuleCollection:IModuleCollection
    {
        /// <summary>
        /// 模块集合
        /// </summary>
        public IReadOnlyList<ModuleInfo>? ModuleInfoList { get; private set; }

        /// <summary>
        /// 循环依赖字典 最后释放的时候需要清除循环依赖记录
        /// </summary>
        private readonly Dictionary<Type, Type> CircleDependDic = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public List<ModuleInfo> BuildModuleDependCollection(Type moduleType)
        {
            List<ModuleInfo> moduleInfos = new();
            // 是抽象 是接口 是泛型 不是一个类(委托等) 是一个类但不是实现IModule接口
            // 即只有类 并且非抽象类 并且是实现了IModule接口的才是模块
            if (moduleType.IsAbstract || moduleType.IsInterface || moduleType.IsGenericType
                || !moduleType.IsClass || (moduleType.IsClass && !typeof(IModule).IsAssignableFrom(moduleType)))
            {
                throw new Exception("依赖异常:" + moduleType.FullName);
            }

            // 当前模块是否有DependsOnModules特性
            DependsOnModulesAttribute? dependsOnModulesAttribute = moduleType.GetCustomAttribute<DependsOnModulesAttribute>();
            // 依赖属性为空
            if (dependsOnModulesAttribute == null)
            {
                moduleInfos.Add(new ModuleInfo(moduleType));
            }
            else
            {
                //判断是否存在循环依赖
                if (CircleDependDic.ContainsKey(moduleType))
                    throw new Exception("模块出现循环依赖:" + moduleType.FullName);

                //存在依赖 把该moduleType加入到判断循环依赖的字典中
                CircleDependDic.Add(moduleType, moduleType);

                // 依赖属性不为空,递归获取依赖
                List<ModuleInfo> dependModuleInfos = new();
                if (dependsOnModulesAttribute.DependsOnModules != null)
                { 
                    foreach (var dependModuleType in dependsOnModulesAttribute.DependsOnModules)
                    {
                        dependModuleInfos.AddRange(
                            BuildModuleDependCollection(dependModuleType)
                        );
                    }
                }
                // 创建模块信息模块以及依赖
                moduleInfos.Add(new ModuleInfo(moduleType, dependModuleInfos.ToArray()));
            }

            return moduleInfos;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public List<ModuleInfo> ModuleSort<TModule>() where TModule : IModule
        {
            //构建依赖集
            List<ModuleInfo> moduleInfos = BuildModuleDependCollection(typeof(TModule));

            //拓扑排序
            List<ModuleInfo> moduleInfoSortList = ModuleTopoSort.Sort(moduleInfos, moduleInfo => moduleInfo.DependsOnModulesInfos);

            return moduleInfoSortList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        public void FindModule<TModule>(IServiceCollection services) where TModule : IModule
        {
            List<ModuleInfo> moduleInfos = new();

            List<ModuleInfo> moduleInfoList = this.ModuleSort<TModule>();
            // 去除重复并加入到service中
            foreach (var item in moduleInfoList)
            {
                if (moduleInfos.Any(moduleInfo => moduleInfo.ModuleType.FullName == item.ModuleType.FullName))
                {
                    continue;
                }
                moduleInfos.Add(item);
                services.AddSingleton(item.ModuleType, item!.Instance!);
            }
            ModuleInfoList = moduleInfos.AsReadOnly();
        }
    }
}
