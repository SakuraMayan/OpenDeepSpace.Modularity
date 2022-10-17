using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.Modularity.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用模块
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseModule(this IApplicationBuilder applicationBuilder)
        {


            //执行Configure方法
            IModuleManager moduleManager = applicationBuilder.ApplicationServices.GetRequiredService<IModuleManager>();
            moduleManager.Configure(applicationBuilder);

            return applicationBuilder;
        }


    }
}
