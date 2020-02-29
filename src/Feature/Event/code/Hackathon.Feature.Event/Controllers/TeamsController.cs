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
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;
            var teamList = item.Children;
            Team
            return View();
        }
    }
}