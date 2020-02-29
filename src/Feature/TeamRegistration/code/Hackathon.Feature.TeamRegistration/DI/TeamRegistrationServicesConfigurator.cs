using Sitecore.Framework.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Hackathon.Feature.TeamRegistration.Services;
using Hackathon.Feature.TeamRegistration.Models;

namespace Hackathon.Feature.TeamRegistration.DI
{
    public class TeamRegistrationServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMessageHandler<Registration>, TeamRegistrationHandler>();
        }
    }
}
