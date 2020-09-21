using Microsoft.AspNetCore.Mvc.HttpActionResultsExtension;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Class containing redirection HTTP response extensions methods for <see cref="ControllerBase"/>.
    /// </summary>
    public static class RedirectionControllerResultExtensions
    {
        #region 303 SeeOther

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to an action with the same name as current one.
        /// The 'controller' and 'action' names are retrieved from the ambient values of the current request.
        /// </summary>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        /// <example>
        /// A POST/PUT/DELETE request to an action named "Product" updates a product and redirects to an action, also named
        /// "Product", showing details of the updated product.
        /// <code>
        /// [HttpGet]
        /// public IActionResult Product(int id)
        /// {
        ///     var product = RetrieveProduct(id);
        ///     return View(product);
        /// }
        ///
        /// [HttpPost]
        /// public IActionResult Product(int id, Product product)
        /// {
        ///     UpdateProduct(product);
        ///     return SeeOther();
        /// }
        /// </code>
        /// </example>
        [NonAction]
        public static SeeOtherResult SeeOther(this ControllerBase controller)
            => SeeOther(controller, actionName: null);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the <paramref name="actionName"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(this ControllerBase controller, string actionName)
            => SeeOther(controller, actionName, routeValues: null);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the
        /// <paramref name="actionName"/> and <paramref name="routeValues"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">The parameters for a route.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(this ControllerBase controller, string actionName, object routeValues)
            => SeeOther(controller, actionName, controllerName: null, routeValues: routeValues);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the
        /// <paramref name="actionName"/> and the <paramref name="controllerName"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(this ControllerBase controller, string actionName, string controllerName)
            => SeeOther(controller, actionName, controllerName, routeValues: null);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the specified
        /// <paramref name="actionName"/>, <paramref name="controllerName"/>, and <paramref name="routeValues"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The parameters for a route.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(
            this ControllerBase controller,
            string actionName,
            string controllerName,
            object routeValues)
            => SeeOther(controller, actionName, controllerName, routeValues, fragment: null);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the specified
        /// <paramref name="actionName"/>, <paramref name="controllerName"/>, and <paramref name="fragment"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="fragment">The fragment to add to the URL.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(
            this ControllerBase controller,
            string actionName,
            string controllerName,
            string fragment)
            => SeeOther(controller, actionName, controllerName, routeValues: null, fragment: fragment);

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to the specified action using the specified <paramref name="actionName"/>,
        /// <paramref name="controllerName"/>, <paramref name="routeValues"/>, and <paramref name="fragment"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The parameters for a route.</param>
        /// <param name="fragment">The fragment to add to the URL.</param>
        /// <returns>The created <see cref="SeeOtherResult"/> for the response.</returns>
        [NonAction]
        public static SeeOtherResult SeeOther(
            this ControllerBase controller,
            string actionName,
            string controllerName,
            object routeValues,
            string fragment)
        {
            return new SeeOtherResult(actionName, controllerName, routeValues, fragment)
            {
                UrlHelper = controller.Url,
            };
        }
        #endregion 303 SeeOther

        /// <summary>
        /// Redirects (<see cref="StatusCodes.Status303SeeOther"/>) to an action with the same name as current one.
        /// The 'controller' and 'action' names are retrieved from the ambient values of the current request.
        /// </summary>
        /// <param name="controller">MVC controller instance.</param>
        /// <returns>The created <see cref="NotModifiedResult"/> for the response.</returns>
        public static NotModifiedResult NotModified(this ControllerBase controller) => new NotModifiedResult();
    }
}
