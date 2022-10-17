using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 模块信息 
    /// 存储模块以及相应模块的依赖模块
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// 模块实例
        /// </summary>
        private object? ModuleInstance;

        /// <summary>
        /// 模块类型
        /// </summary>
        public Type ModuleType { get; private set; }

        /// <summary>
        /// 依赖模块
        /// </summary>
        public ModuleInfo[]? DependsOnModulesInfos { get; private set; }

        /// <summary>
        /// 实例
        /// </summary>
        public object? Instance
        {
            get
            {
                if (this.ModuleInstance == null)
                {
                    this.ModuleInstance = Activator.CreateInstance(this.ModuleType);
                }
                return this.ModuleInstance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModuleType"></param>
        /// <param name="DependsOnModuleInfos"></param>
        public ModuleInfo(Type ModuleType, params ModuleInfo[] DependsOnModuleInfos)
        {
            this.ModuleType = ModuleType;
            this.DependsOnModulesInfos = DependsOnModuleInfos;
        }
    }
}
