using OpenDeepSpace.Modularity.Attributes;

namespace OpenDeepSpace.Modularity.Demo.Modules
{
    [DependsOnModules(typeof(ModuleB),typeof(ModuleC))]
    public class ModuleA : AbstractModule
    {
        private  ILogger<ModuleA> logger;

        public override void ConfigureServices(IServiceCollection services)
        {
            logger = services.BuildServiceProvider().GetRequiredService<ILogger<ModuleA>>();
            logger.LogInformation($"{nameof(ModuleA)}.{nameof(ConfigureServices)}");
        }

        public override void Configure(IApplicationBuilder app)
        {
            logger.LogInformation($"{nameof(ModuleA)}.{nameof(Configure)}");
        }
    }
}
