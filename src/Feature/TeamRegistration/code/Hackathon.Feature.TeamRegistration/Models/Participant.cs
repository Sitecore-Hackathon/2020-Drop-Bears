using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Feature.TeamRegistration.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Slack { get; set; }
        public string Github { get; set; }
        public string Location { get; set; }

    }
}