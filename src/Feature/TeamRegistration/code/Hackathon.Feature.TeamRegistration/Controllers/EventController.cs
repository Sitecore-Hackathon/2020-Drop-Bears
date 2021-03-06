﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Controllers;
using Hackathon.Feature.TeamRegistration.Models;
using Hackathon.Feature.TeamRegistration.Models.ViewModel;
using Sitecore.Data.Items;

namespace Hackathon.Feature.TeamRegistration.Controllers
{
    public class EventController : SitecoreController
    {
        // GET: Default
        public ActionResult Event()
        {
            var viewModel = new EventViewModel();
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;

            viewModel.Event = new Event();
            viewModel.Event.Name = item?.Fields["Event Name"]?.Value;
            viewModel.Event.Description = item?.Fields["Description"]?.Value;

            var hashTagList = item?.Fields["Hashtags"]?.Value.ToString().Split(' ').ToList();
            viewModel.Event.Hashtags = hashTagList;

            var startDate = item?.Fields["Start Date"]?.Value;
            viewModel.Event.StartDate = Sitecore.DateUtil.IsoDateToDateTime(startDate);

            var endDate = item?.Fields["End Date"]?.Value;
            viewModel.Event.EndDate = Sitecore.DateUtil.IsoDateToDateTime(endDate);

            var topics = item?.Fields["Topics"]?.Value;
            var topicDate = item?.Fields["Topic Release Date"].ToString();
            if(!string.IsNullOrEmpty(topics) || !string.IsNullOrEmpty(topicDate))
            {
                viewModel.Event.TopicReleaseDate = Sitecore.DateUtil.IsoDateToDateTime(topicDate);
                var topicRelease = Sitecore.DateUtil.IsoDateToDateTime(topicDate);
                if(topicRelease != null)
                {
                    if(topicRelease <= DateTime.UtcNow)
                    {
                        viewModel.TopicReady = true;
                        viewModel.Event.Topics = topics;
                    }
                }
            }

            return View(viewModel);
        }
    }
}