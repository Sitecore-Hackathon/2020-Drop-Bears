using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Hackathon.Feature.TeamRegistration.Controllers
{
    public class Teams : SitecoreController
    {
        // GET: Default
        public ActionResult List()
        {

            return View();
        }
    }
}