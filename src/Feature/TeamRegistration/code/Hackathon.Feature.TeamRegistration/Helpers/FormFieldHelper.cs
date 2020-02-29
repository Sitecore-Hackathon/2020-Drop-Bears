using Sitecore.ExperienceForms.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Feature.TeamRegistration.Helpers
{
    public  class FormFieldHelper
    {
        private FormSubmitContext _formSubmitContext;

        public FormFieldHelper(FormSubmitContext formSubmitContext)
        {
            _formSubmitContext = formSubmitContext;
        }

        public string GetFieldValue(string name, int iteration)
        {
            return GetValue(_formSubmitContext.Fields.Where(f => f.Name.Equals(name)).Skip(iteration).FirstOrDefault());
        }

        public string GetFieldValue(string name)
        {
            return GetValue(_formSubmitContext.Fields.FirstOrDefault(f => f.Name.Equals(name)));
        }

        private string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }

        internal int Count(string name)
        {
            return _formSubmitContext.Fields.Where(f => f.Name.Equals(name)).Count();
        }
    }
}