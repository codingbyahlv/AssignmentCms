using AssignmentCms.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace AssignmentCms.Controllers
{
    public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        [HttpPost]
        public IActionResult HandleSubmit(ContactFormModel form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["message"] = form.Message;

                ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
                ViewData["error_message"] = string.IsNullOrEmpty(form.Message);

                //Lägg in hantering till api osv här

                return CurrentUmbracoPage();
            }

            TempData["success"] = "Form submitted!";
            return RedirectToCurrentUmbracoPage();

        }

        [HttpPost]
        public IActionResult HandleSubmitHelpSmall(SmallContactFormModel formSmall)
        {

           if (!ModelState.IsValid)
            {

                ViewData["email"] = formSmall.Email;
                ViewData["error_email_small"] = string.IsNullOrEmpty(formSmall.Email);

                //Lägg in hantering till api osv här

                return CurrentUmbracoPage();
            }

            TempData["successSmall"] = "Form submitted!";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
