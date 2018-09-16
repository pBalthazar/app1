using System.Web.Mvc;

public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
{
    protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
    {
        filterContext.Result = new StatusCodeResult(400);
    }
}