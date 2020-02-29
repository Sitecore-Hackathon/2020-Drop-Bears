using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitRegistrationToQueue"/> class.
        /// </summary>
        /// <param name="submitActionData">The submit action data.</param>
        public SubmitRegistrationToQueue(ISubmitActionData submitActionData, TeamRegistrationRepository registrationRepository) : base(submitActionData)
        {
            _registrationRepository = registrationRepository;
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

            //TODO: create object based on form fields.
            //var blah = formSubmitContext.Fields["FirstName"];

            //MOCK out the object
            var registration = new Registration()
            {
                Event = new Event() { Name = "Hackathon 2020" },
                Team = new Team() { RepositoryName = "DropBears",
                    RepositoryUrl = "https://github.com/DropBears",
                    Slogan = "Drop Bears will eat you alive!",
                    TeamName = $"DropBears {System.Guid.NewGuid().ToString()}",
                    Members = new List<Participant>() {  
                        new Participant() {  Name = "George Tucker",
                            Email = "gwdt@example.com",
                            Github = "gwdt",
                            LinkedIn = "gwdtucker",
                            Location ="Perth, Australia",
                            Slack = "gwdt",
                            Twitter = "gwdt"
                        },
                        new Participant() {  Name = "Matthew Ellins",
                            Email = "mellins@example.com",
                            Github = "mellins",
                            LinkedIn = "matt.ellins",
                            Location ="Perth, Australia",
                            Slack = "m.ellins",
                            Twitter = "spyn"
                        },
                        new Participant() {  Name = "Andy Parry",
                            Email = "aparry@example.com",
                            Github = "aparry",
                            LinkedIn = "andrewparry",
                            Location ="Perth, Australia",
                            Slack = "aparry",
                            Twitter = "aparry"
                        }
                    }
                },

            };

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