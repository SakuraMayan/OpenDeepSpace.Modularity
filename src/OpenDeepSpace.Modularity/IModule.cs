using Microsoft.AspNetCore.Builder;

namespace OpenDeepSpace.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModule
    {
        public void Configure(IApplicationBuilder app);
    }
}