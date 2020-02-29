using System;
using System.Collections.Generic;

namespace Hackathon.Feature.TeamRegistration.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Topics { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TopicReleaseDate { get; set; }
        public IList<string> Hashtags { get; set; }
    }
}