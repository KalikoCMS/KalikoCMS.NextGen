namespace KalikoCMS.ServiceLocation.Interfaces {
    using Microsoft.Extensions.DependencyInjection;

    public interface IDependencyInjectionRegistrator {
        void Register(IServiceCollection services);
    }
}
