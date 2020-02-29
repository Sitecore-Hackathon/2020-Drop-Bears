using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Hackathon.Feature.Event.Controllers
{
    public class TeamsController : SitecoreController
    {
        // GET: Lists
        public ActionResult List(string itemId)
        {
//            var render = new Sitecore.Mvc.Presentation.RenderingModel();
//            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;

            var web = Sitecore.Configuration.Factory.GetDatabase("web");
            var teams = web.GetItem(itemId);
            var teamList = teams.GetChildren();           

            return View();
        }
    }
}