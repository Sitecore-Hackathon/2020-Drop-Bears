using Sitecore.Framework.Messaging;
using Hackathon.Feature.TeamRegistration.Models;
using Sitecore.Data.Items;
using Hackathon.Feature.TeamRegistration.Helpers;
using Sitecore.DependencyInjection;
using Sitecore.Workflows;

namespace Hackathon.Feature.TeamRegistration.Services
{
    public class TeamRegistrationRepository
    {
        const string _teamTemplate = "{1D23F767-F6F3-464B-99C9-28307D7B27D5}";
        const string _participantTemplate = "{52DBE4B0-A6C2-45CC-A574-E10CEDEC3D65}";
        const string _workflowID = "{3247B40E-0249-457F-BD4C-2D1A0126DEE2}";
        const string eventsPath = "/sitecore/content/Events/";

        private readonly IMessageBus<RegistrationMessageBus> messageBus;
        public TeamRegistrationRepository()
        {   
            this.messageBus = (IMessageBus<RegistrationMessageBus>)ServiceLocator.ServiceProvider.GetService(typeof(IMessageBus<RegistrationMessageBus>));
        }

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
            Sitecore.Data.Database masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
            
            var eventItemName = StringHelpers.CleanEventName(registration.Event.Name);
            Item eventItem = masterDB.GetItem($"{eventsPath}{eventItemName}");
            if (eventItem == null)
                return;
           
            var teamTemplate = masterDB.GetTemplate(_teamTemplate);

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var teamItemName = StringHelpers.CleanTeamName(registration.Team.TeamName);

                //No dupes pls.
                if (masterDB.GetItem($"{eventsPath}{eventItemName}/{teamItemName}") != null)
                {
                    teamItemName = StringHelpers.CleanTeamName($"{System.Guid.NewGuid().ToString()}_{teamItemName}");
                }

                Item teamItem = eventItem.Add(teamItemName, teamTemplate);
                if (teamItem != null)
                {
                    teamItem.Editing.BeginEdit();
                    teamItem["Team Name"] = registration.Team.TeamName;
                    teamItem["Slogan"] = registration.Team.Slogan;
                    teamItem["Repository Name"] = registration.Team.RepositoryName;
                    teamItem["Repository Url"] = registration.Team.RepositoryUrl;
                    teamItem.Fields[Sitecore.FieldIDs.Workflow].Value = _workflowID;
                    IWorkflow wf = masterDB.WorkflowProvider.GetWorkflow(_workflowID);
                    wf.Start(teamItem);
                    teamItem.Editing.EndEdit();
                }
                var memberTemplate = masterDB.GetTemplate(_participantTemplate);
                foreach (Participant member in registration.Team.Members)
                {
                    var participantItemName = StringHelpers.CleanParticipantName(member.Name);
                    Item participantItem  = teamItem.Add(participantItemName, memberTemplate);
                    participantItem.Editing.BeginEdit();
                    participantItem["Participant Name"] = member.Name;
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