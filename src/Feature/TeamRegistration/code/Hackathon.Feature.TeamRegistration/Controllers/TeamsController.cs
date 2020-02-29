using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Controllers;
using Hackathon.Feature.TeamRegistration.Models;
using Sitecore.Data.Items;

namespace Hackathon.Feature.TeamRegistration.Controllers
{
    public class TeamsController : SitecoreController
    {
        // GET: Default
        public ActionResult List()
        {
            var viewModel = new List<Team>();
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;
            var teamList = item.GetChildren().ToList<Item>();
            foreach (var teamItem in teamList)
            {
                var team = new Team();
                
                team.TeamName = teamItem?.Fields["Team Name"]?.Value;
                team.RepositoryName = teamItem?.Fields["Repository Name"]?.Value;
                team.RepositoryUrl = teamItem?.Fields["Repository Url"]?.Value;
                team.Members = new List<Participant>();

                var members = teamItem.GetChildren().ToList<Item>();
                foreach (var memberItem in members)
                {
                    var member = new Participant()
                    {
                        Name = teamItem?.Fields["Participant Name"]?.Value,
                        Email = teamItem?.Fields["Email"]?.Value,
                        Twitter = teamItem?.Fields["Twitter"]?.Value,
                        LinkedIn = teamItem?.Fields["LinkedIn"]?.Value,
                        Slack = teamItem?.Fields["Slack"]?.Value,
                        Github = teamItem?.Fields["Github"]?.Value,
                        Location = teamItem?.Fields["Location"]?.Value
                    };
                    team.Members.Add(member);
                }
                viewModel.Add(team);
            }
            return View(viewModel);
        }
    }
}