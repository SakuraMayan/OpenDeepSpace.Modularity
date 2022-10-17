using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity.Attributes
{
    /// <summary>
    /// 模块依赖特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnModulesAttribute:Attribute
    {
        /// <summary>
        /// 依赖模块类型
        /// </summary>
        public Type[]? DependsOnModules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DependsOnModules"></param>
        public DependsOnModulesAttribute(params Type[] DependsOnModules)
        {
            this.DependsOnModules = DependsOnModules;
        }
    }
}
