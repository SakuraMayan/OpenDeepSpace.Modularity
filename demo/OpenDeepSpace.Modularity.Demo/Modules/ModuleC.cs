namespace OpenDeepSpace.Modularity.Demo.Modules
{
    public class ModuleC : AbstractModule
    {

        private ILogger<ModuleC> logger;
        public override void ConfigureServices(IServiceCollection services)
        {
            logger = services.BuildServiceProvider().GetRequiredService<ILogger<ModuleC>>();
            logger.LogInformation($"{nameof(ModuleC)}.{nameof(ConfigureServices)}");
        }

        public override void Configure(IApplicationBuilder app)
        {
            logger.LogInformation($"{nameof(ModuleC)}.{nameof(Configure)}");
        }
    }
}
