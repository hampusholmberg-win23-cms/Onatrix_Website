using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCMS.Models;
using UmbracoCMS.Services;

namespace UmbracoCMS.Controllers
{
    public class EmailFormSurfaceController : SurfaceController
    {
        private readonly EmailService _emailService;
        public EmailFormSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, EmailService emailService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _emailService = emailService;
        }

        public async Task<IActionResult> HandleSubmit(EmailFormModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = model.Name;
                ViewData["email"] = model.Email;
                ViewData["message"] = model.Message;

                ViewData["name_error"] = string.IsNullOrEmpty(model.Name) ? "required" : null;
                ViewData["email_error"] = string.IsNullOrEmpty(model.Email) ? "required" : null;
                ViewData["phone_message"] = string.IsNullOrEmpty(model.Message) ? "required" : null;

                return CurrentUmbracoPage();
            }

            var result = await _emailService.EmailFormEmailConfirmationAsync(model);

            TempData["success"] = "Success";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
