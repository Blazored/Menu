using Microsoft.AspNetCore.Components;

namespace Blazored.Menu
{
    public class BlazoredSubMenuBase : ComponentBase
    {
        [Parameter] protected string Header { get; set; }
        [Parameter] protected string IconClass { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string Css { get; set; } = "hidden";
        protected string Icon { get; set; } = "+";

        protected void ToggleSubMenu()
        {
            Css = Css == "hidden" ? "visible" : "hidden";
        }
    }
}
