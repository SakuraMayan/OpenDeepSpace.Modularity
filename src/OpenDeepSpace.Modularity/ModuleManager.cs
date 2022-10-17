using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 模块管理者 集中调用所有模块信息
    /// </summary>
    public class ModuleManager : IModuleManager
    {
        private ModuleCollection? ModuleCollection;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
           

            ModuleCollection = services.BuildServiceProvider().GetRequiredService<ModuleCollection>();

            var moduleInfoList = ModuleCollection.ModuleInfoList;

            if (moduleInfoList != null)
            { 
            
                foreach (ModuleInfo moduleInfo in moduleInfoList)
                {

                   
                    (moduleInfo.Instance as AbstractModule)?.ConfigureServices(services);
                    
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            //是否依据中间件的模式 先进后出 先实例化的后执行这个方法 ModuleCollection.ModuleInfoList.Reverse()
            //先采用非中间件模式
            var moduleInfoList = ModuleCollection!.ModuleInfoList;
            if (moduleInfoList != null)
            {
                //moduleInfoList = moduleInfoList.Reverse().ToList();//先进后出模式
                
                foreach (ModuleInfo moduleInfo in moduleInfoList)
                {
                    
                    (moduleInfo.Instance as AbstractModule)?.Configure(app);

                }
            }
        }

    }
}
