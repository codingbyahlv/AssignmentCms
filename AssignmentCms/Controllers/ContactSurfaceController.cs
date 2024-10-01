using AssignmentCms.Models;
using AssignmentCms.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace AssignmentCms.Controllers
{
	public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, EmailService emailService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
	{
		private readonly EmailService _emailService = emailService;

		[HttpPost]
		public async Task<IActionResult> HandleSubmit(ContactFormModel form)
		{
			if (!ModelState.IsValid)
			{
				ViewData["name"] = form.Name;
				ViewData["email"] = form.Email;
				ViewData["message"] = form.Message;

				ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
				ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
				ViewData["error_message"] = string.IsNullOrEmpty(form.Message);

				return CurrentUmbracoPage();
			}

			if (await _emailService.SendEmailConfirmationAsync(form.Email))
				TempData["success"] = "Form submitted!";
			else
				TempData["error"] = "Something went wrong. Please try later";

			return RedirectToCurrentUmbracoPage();


		}

		[HttpPost]
		public async Task<IActionResult> HandleSubmitHelpSmall(SmallContactFormModel formSmall)
		{
			if (!ModelState.IsValid)
			{

				ViewData["email"] = formSmall.Email;
				ViewData["error_email_small"] = string.IsNullOrEmpty(formSmall.Email);

				return CurrentUmbracoPage();
			}

			if (await _emailService.SendEmailConfirmationAsync(formSmall.Email))
				TempData["successSmall"] = "Form submitted!";
			else
				TempData["errorSmall"] = "Something went wrong. Please try later";

			return RedirectToCurrentUmbracoPage();
		}
	}
}
