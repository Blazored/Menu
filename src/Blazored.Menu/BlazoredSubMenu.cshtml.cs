using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Blazored.Menu
{
    public class BlazoredSubMenuBase : ComponentBase
    {
        [Parameter] protected string Header { get; set; }
        [Parameter] protected string IconClass { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected RenderFragment HeaderTemplate { get; set; }
        [Parameter] protected RenderFragment MenuTemplate { get; set; } 
        [Parameter] protected string Css { get; set; }
        [Parameter] protected bool IsEnabled { get; set; } = true;
        [Parameter] protected IEnumerable<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        protected string Icon { get; set; } = "+";
        protected bool IsOpen { get; set; }
        protected string CssString
        {
            get
            {
                var cssString = "blazored-sub-menu-header";

                cssString += $" {Css}";
                cssString += IsOpen ? " open" : "";

                return cssString.Trim();
            }
        }

        protected void ToggleSubMenu()
        {
            IsOpen = !IsOpen;
            Icon = IsOpen ? "-" : "+";
        }
    }
}
