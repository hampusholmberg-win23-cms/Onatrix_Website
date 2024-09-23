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

namespace UmbracoCMS.Controllers;

public class OnlineSupportBoxSurfaceController : SurfaceController
{
    private readonly EmailService _emailService;

    public OnlineSupportBoxSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, EmailService emailService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _emailService = emailService;
    }

    public async Task<IActionResult> HandleSubmit(OnlineSupportFormModel model)
    {
        var result = await _emailService.OnlineSupportEmailConfirmation(model);

        TempData["online_support_box_success"] = "Success";
        return RedirectToCurrentUmbracoPage();
    }
}
