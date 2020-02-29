using Sitecore.Framework.Messaging;
using Hackathon.Feature.TeamRegistration.Models;
using Sitecore.Data.Items;

namespace Hackathon.Feature.TeamRegistration.Services
{
    public class TeamRegistrationRepository
    {
        private readonly IMessageBus<RegistrationMessageBus> messageBus;
        public TeamRegistrationRepository(IMessageBus<RegistrationMessageBus> messageBus)
        {
            this.messageBus = messageBus;
        }
        public virtual void Send(Registration registration)
        {
            this.messageBus.SendAsync(registration);
        }

        public virtual void CreateItem(Registration registration)
        {
            //TODO: Un-hardcode the things.  Branch template?
            Sitecore.Data.Database masterDB = Sitecore.Configuration.Factory.GetDatabase("master");

            Item evenTeamsItem = masterDB.GetItem($"/sitecore/content/home/{registration.Event.Name}/Teams");
           
            var teamTemplate = masterDB.GetTemplate("/sitecore/templates/Feature/Hackathon/Team");

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                Item teamItem = evenTeamsItem.Add(registration.Team.TeamName, teamTemplate);
                if (teamItem != null)
                {
                    teamItem.Editing.BeginEdit();
                    teamItem["Slogan"] = registration.Team.Slogan;
                    teamItem["RepositoryName"] = registration.Team.RepositoryName;
                    teamItem["RepositoryUrl"] = registration.Team.RepositoryUrl;
                    teamItem.Editing.EndEdit();
                }
                var memberTemplate = masterDB.GetTemplate("/sitecore/templates/Feature/Hackathon/participant");
                foreach (Participant member in registration.Team.Members)
                {
                    Item participantItem  = teamItem.Add(member.Name, memberTemplate);
                    participantItem.Editing.BeginEdit();
                    participantItem["Email"] = member.Email;
                    participantItem["Location"] = member.Location;
                    participantItem["Github"] = member.Github;
                    participantItem["LinkedIn"] = member.LinkedIn;
                    participantItem["Slack"] = member.Slack;
                    participantItem["Twitter"] = member.Twitter;
                    participantItem.Editing.EndEdit();
                }
                
            }

        }
    }
}