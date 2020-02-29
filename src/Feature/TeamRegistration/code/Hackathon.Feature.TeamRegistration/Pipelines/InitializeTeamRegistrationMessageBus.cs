using System;
using Sitecore.Framework.Messaging;
using Sitecore.Pipelines;

namespace Hackathon.Feature.TeamRegistration.Pipelines
{
    public class InitializeTeamRegistrationMessageBus
    {
        private readonly IServiceProvider serviceProvider;
        public InitializeTeamRegistrationMessageBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Process(PipelineArgs args)
        {
            this.serviceProvider.StartMessageBus();
        }
    }
}