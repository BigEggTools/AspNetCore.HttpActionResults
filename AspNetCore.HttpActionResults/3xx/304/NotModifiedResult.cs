using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Mvc.HttpActionResultsExtension
{
    /// <summary>
    /// An <see cref="StatusCodeResult"/> that when executed will produce an empty
    /// <see cref="StatusCodes.Status304NotModified"/> response.
    /// </summary>
    public class NotModifiedResult : StatusCodeResult
    {
        private const int DefaultStatusCode = StatusCodes.Status304NotModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotModifiedResult"/> class.
        /// </summary>
        /// <remarks>https://tools.ietf.org/html/rfc7232</remarks>
        public NotModifiedResult()
            : base(DefaultStatusCode)
        {
        }
    }
}
