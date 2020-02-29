using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Feature.TeamRegistration.Helpers
{
    public static class StringHelpers
    {
        public static string CleanTeamName(string inputTeamName)
        {
            return SitecoreItemifyString(inputTeamName);
        }

        public static string CleanEventName(string inputEventName)
        {
            return SitecoreItemifyString(inputEventName);
        }

        public static string CleanParticipantName(string inputParticipantName)
        {
            return SitecoreItemifyString(inputParticipantName);
        }

        private static string SitecoreItemifyString(string inputTeamName)
        {
            return Sitecore.Data.Items.ItemUtil.ProposeValidItemName(inputTeamName, inputTeamName);
        }
    }
}