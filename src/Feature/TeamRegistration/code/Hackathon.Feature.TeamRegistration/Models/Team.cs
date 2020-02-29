using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Feature.TeamRegistration.Models
{
    public class Team
    {
        public string TeamName { get; set; }
        public List<Participant> Members { get; set; }
        public string Slogan { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }
    }
}