using System.Collections.Generic;
using System.Linq;
using Hackathon.Feature.TeamRegistration.Helpers;
using Hackathon.Feature.TeamRegistration.Models;
using Hackathon.Feature.TeamRegistration.Services;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using static System.FormattableString;

namespace Hackathon.Feature.TeamRegistration
{
    public class SubmitRegistrationToQueue : SubmitActionBase<string>
    {
        private TeamRegistrationRepository _registrationRepository;
        const string _githubUrl = "https://github.com";

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitRegistrationToQueue"/> class.
        /// </summary>
        /// <param name="submitActionData">The submit action data.</param>
        public SubmitRegistrationToQueue(ISubmitActionData submitActionData) : base(submitActionData)
        {
            _registrationRepository = new TeamRegistrationRepository();
        }

        /// <summary>
        /// Tries to convert the specified <paramref name="value" /> to an instance of the specified target type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="target">The target object.</param>
        /// <returns>
        /// true if <paramref name="value" /> was converted successfully; otherwise, false.
        /// </returns>
        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
        }

        /// <summary>
        /// Executes the action with the specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="formSubmitContext">The form submit context.</param>
        /// <returns>
        ///   <c>true</c> if the action is executed correctly; otherwise <c>false</c>
        /// </returns>
        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
            
            var formHelper = new FormFieldHelper(formSubmitContext);

            //This is gross. 🤮
            var eventField = formSubmitContext.Fields.FirstOrDefault(f => f.Name.Equals("Event"));
            var eventName = ((IEnumerable<string>)eventField
                .GetType()
                .GetProperty("Value")?
                .GetValue(eventField, null)).FirstOrDefault();

            if (eventName == null)
                return false;

            var teamName = formHelper.GetFieldValue("TeamName");
            var registration = new Registration()
            {
                Event = new Event() { Name = eventName.ToString() },
                Team = new Team() {
                    //EG: https://github.com/Sitecore-Hackathon/2020-Drop-Bears
                    RepositoryUrl = $"{_githubUrl}/{StringHelpers.CleanTeamName(eventName.ToString())}/{StringHelpers.CleanTeamName(teamName)}",
                    Slogan = formHelper.GetFieldValue("TeamSlogan"),
                    TeamName = teamName,
                    Members = new List<Participant>()

                }

            };

            //Add members
            for (int i=0;  i < formHelper.Count("Name"); i++)
            {
                registration.Team.Members.Add(
                            new Participant()
                            {
                                Name = formHelper.GetFieldValue("Name",i),
                                Email = formHelper.GetFieldValue("Email",i),
                                Github = formHelper.GetFieldValue("Github", i),
                                LinkedIn = formHelper.GetFieldValue("LinkedIn", i),
                                Location = formHelper.GetFieldValue("Location", i),
                                Slack = formHelper.GetFieldValue("Slack", i),
                                Twitter = formHelper.GetFieldValue("Twitter", i)
                            });
            }

            if (!formSubmitContext.HasErrors)
            {
                _registrationRepository.Send(registration);

                Logger.Info(Invariant($"Form {formSubmitContext.FormId} submitted successfully."), this);
            }
            else
            {
                Logger.Warn(Invariant($"Form {formSubmitContext.FormId} submitted with errors: {string.Join(", ", formSubmitContext.Errors.Select(t => t.ErrorMessage))}."), this);
                
            }

            return true;
        }

        
    }
}