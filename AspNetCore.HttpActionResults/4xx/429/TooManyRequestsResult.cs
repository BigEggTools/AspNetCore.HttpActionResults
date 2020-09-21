using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.HttpActionResultsExtension
{
    /// <summary>
    /// An <see cref="ActionResult"/> that returns a Too Many Requests (429) response.
    /// Targets a controller action.
    /// </summary>
    public class TooManyRequestsResult : ActionResult, IKeepTempDataResult
    {
        private int _limit;
        private int _period;

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsResult"/> class.
        /// provided.
        /// </summary>
        public TooManyRequestsResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsResult"/> class.
        /// </summary>
        /// <remarks>https://tools.ietf.org/html/rfc6585</remarks>
        /// <param name="limit">The maximum number of requests that a client can make in a defined period.</param>
        /// <param name="period">The rate limit period (in seconds).</param>
        public TooManyRequestsResult(int limit, int period)
        {
            _limit = limit;
            _period = period;
        }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            if (_period > 0)
            {
                context.HttpContext.Response.ContentType = "text/plain";
                context.HttpContext.Response.Headers[HeaderNames.RetryAfter] = _period.ToString(CultureInfo.CurrentCulture);
                context.HttpContext.Response.WriteAsync($"API calls quota exceeded! Maximum permitted {_limit} per {_period} seconds.");
            }

            return Task.CompletedTask;
        }
    }
}
