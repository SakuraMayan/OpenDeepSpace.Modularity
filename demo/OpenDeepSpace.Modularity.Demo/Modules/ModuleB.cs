using OpenDeepSpace.Modularity.Attributes;

namespace OpenDeepSpace.Modularity.Demo.Modules
{
    [DependsOnModules(typeof(ModuleC))]
    public class ModuleB : AbstractModule
    {
        private ILogger<ModuleB> logger;
        public override void ConfigureServices(IServiceCollection services)
        {
            logger = services.BuildServiceProvider().GetRequiredService<ILogger<ModuleB>>();
            logger.LogInformation($"{nameof(ModuleB)}.{nameof(ConfigureServices)}");
        }

        public override void Configure(IApplicationBuilder app)
        {
            logger.LogInformation($"{nameof(ModuleB)}.{nameof(Configure)}");
        }
    }
}
