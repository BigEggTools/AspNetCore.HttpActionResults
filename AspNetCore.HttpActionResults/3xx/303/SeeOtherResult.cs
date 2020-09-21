using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.HttpActionResultsExtension
{
    /// <summary>
    /// An <see cref="ActionResult"/> that returns a See Other (303) response with a Location header.
    /// Targets a controller action.
    /// </summary>
    public class SeeOtherResult : ActionResult, IKeepTempDataResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeeOtherResult"/> with the values
        /// provided.
        /// </summary>
        /// <param name="actionName">The name of the action to use for generating the URL.</param>
        /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        public SeeOtherResult(
            string actionName,
            string controllerName,
            object routeValues)
            : this(actionName, controllerName, routeValues, fragment: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeeOtherResult"/> with the values
        /// provided.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/HTTP_303</remarks>
        /// <param name="actionName">The name of the action to use for generating the URL.</param>
        /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="fragment">The fragment to add to the URL.</param>
        public SeeOtherResult(
            string actionName,
            string controllerName,
            object routeValues,
            string fragment)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues == null ? null : new RouteValueDictionary(routeValues);
            Fragment = fragment;
        }

        /// <summary>
        /// Gets or sets the <see cref="IUrlHelper" /> used to generate URLs.
        /// </summary>
        public IUrlHelper UrlHelper { get; set; }

        /// <summary>
        /// Gets or sets the name of the action to use for generating the URL.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller to use for generating the URL.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the route data to use for generating the URL.
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        /// Gets or sets the fragment to add to the URL.
        /// </summary>
        public string Fragment { get; set; }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var services = context.HttpContext.RequestServices;
            var urlHelperFactory = services.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = UrlHelper ?? urlHelperFactory.GetUrlHelper(context);
            var destinationUrl = urlHelper.Action(
                ActionName,
                ControllerName,
                RouteValues,
                protocol: null,
                host: null,
                fragment: Fragment);
            if (string.IsNullOrEmpty(destinationUrl))
            {
                throw new InvalidOperationException("No route matches the supplied values.");
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status303SeeOther;
            context.HttpContext.Response.Headers[HeaderNames.Location] = destinationUrl;

            return Task.CompletedTask;
        }
    }
}
