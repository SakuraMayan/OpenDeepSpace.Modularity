using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 模块拓扑排序
    /// </summary>
    public static class ModuleTopoSort
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="FindDependModules"></param>
        /// <returns></returns>
        public static List<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>?> FindDependModules) where T : notnull
        {

            List<T> sortList = new();
            Dictionary<T, bool> visitDic = new();

            foreach (var item in source)
            {
                Visit(item, FindDependModules, sortList, visitDic);
            }

            return sortList;
        }

        private static void Visit<T>(T item, Func<T, IEnumerable<T>?> FindDependModules, List<T> sortList, Dictionary<T, bool> visitDic) where T : notnull
        {
            // 如果已经访问该顶点
            if (visitDic.ContainsKey(item))
            {
                //存在循环依赖
                throw new Exception("模块进行拓扑时存在循环依赖:" + nameof(item));
            }
            else
            {
                // 正在处理当前顶点
                visitDic[item] = true;

                // 获得所有依赖项
                var dependModuleTypes = FindDependModules(item);
                // 如果依赖项集合不为空遍历其依赖节点
                if (dependModuleTypes != null)
                {
                    foreach (var dependModuleType in dependModuleTypes)
                    {
                        // 递归遍历访问
                        Visit(dependModuleType, FindDependModules, sortList, visitDic);
                    }
                }

                // 处理完成置为 false
                visitDic[item] = false;
                sortList.Add(item);
            }
        }
    }
}
