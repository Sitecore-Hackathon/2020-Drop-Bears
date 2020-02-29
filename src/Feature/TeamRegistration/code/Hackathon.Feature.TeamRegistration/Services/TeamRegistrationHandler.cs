using System.Threading.Tasks;
using Hackathon.Feature.TeamRegistration.Models;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Managers;
using Sitecore.Framework.Messaging;
using Sitecore.Globalization;

namespace Hackathon.Feature.TeamRegistration.Services
{
    public class TeamRegistrationHandler : IMessageHandler<Registration>
    {
        private readonly TeamRegistrationRepository repository;
        public TeamRegistrationHandler(TeamRegistrationRepository repository)
        {
            this.repository = repository;
        }
        public Task Handle(Registration message, IMessageReceiveContext receiveContext, IMessageReplyContext replyContext)
        {   
            this.repository.CreateItem(message);
            return Task.CompletedTask;
        }


    }
}
