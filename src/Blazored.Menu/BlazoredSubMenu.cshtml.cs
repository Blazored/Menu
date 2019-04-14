using Microsoft.AspNetCore.Components;

namespace Blazored.Menu
{
    public class BlazoredSubMenuBase : ComponentBase
    {
        [Parameter] protected string Header { get; set; }
        [Parameter] protected string IconClass { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string Icon { get; set; } = "+";
        protected bool IsOpen { get; set; }

        protected void ToggleSubMenu()
        {
            IsOpen = !IsOpen;
            Icon = IsOpen ? "-" : "+";
        }
    }
}
