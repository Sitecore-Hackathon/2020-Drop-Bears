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
        // GET: Default
        public ActionResult List()
        {
            var render = new Sitecore.Mvc.Presentation.RenderingModel();
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;

            var db = Sitecore.Context.ContentDatabase;

            //var teams = db.GetItem(itemId);
            //var teamList = teams.GetChildren();           

            return null;
        }
    }
}