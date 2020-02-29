using Sitecore.Framework.Messaging;
using Hackathon.Feature.TeamRegistration.Models;
using Sitecore.Data.Items;

namespace Hackathon.Feature.TeamRegistration.Services
{
    public class TeamRegistrationRepository
    {
        const string _teamTemplate = "{1D23F767-F6F3-464B-99C9-28307D7B27D5}";
        const string _participantTemplate = "{52DBE4B0-A6C2-45CC-A574-E10CEDEC3D65}";

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


            var eventItemName = Sitecore.Data.Items.ItemUtil.ProposeValidItemName(registration.Event.Name, registration.Event.Name);
            Item eventItem = masterDB.GetItem($"/sitecore/content/Events/{eventItemName}");
           
            var teamTemplate = masterDB.GetTemplate(_teamTemplate);

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var teamItemName = Sitecore.Data.Items.ItemUtil.ProposeValidItemName(registration.Team.TeamName, registration.Team.TeamName);
                Item teamItem = eventItem.Add(teamItemName, teamTemplate);
                if (teamItem != null)
                {
                    teamItem.Editing.BeginEdit();
                    teamItem["Team Name"] = registration.Team.TeamName;
                    teamItem["Slogan"] = registration.Team.Slogan;
                    teamItem["Repository Name"] = registration.Team.RepositoryName;
                    teamItem["Repository Url"] = registration.Team.RepositoryUrl;
                    teamItem.Editing.EndEdit();
                }
                var memberTemplate = masterDB.GetTemplate(_participantTemplate);
                foreach (Participant member in registration.Team.Members)
                {
                    Item participantItem  = teamItem.Add(member.Name, memberTemplate);
                    participantItem.Editing.BeginEdit();
                    participantItem["Email"] = member.Email;
                    participantItem["Github"] = member.Github;
                    participantItem["LinkedIn"] = member.LinkedIn;
                    participantItem["Slack"] = member.Slack;
                    participantItem["Twitter"] = member.Twitter;
                    participantItem["Location"] = member.Location;
                    participantItem.Editing.EndEdit();
                }
                
            }

        }
    }
}