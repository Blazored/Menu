using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace Blazored.Menu
{
    public class BlazoredSubMenuBase : ComponentBase
    {
        [Parameter] public string Header { get; set; }
        [Parameter] public string IconClass { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment HeaderTemplate { get; set; }
        [Parameter] public RenderFragment MenuTemplate { get; set; } 
        [Parameter] public string Css { get; set; }
        [Parameter] public bool IsEnabled { get; set; } = true;
        [Parameter] public IEnumerable<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

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

        /// <summary>
        /// Handler for the key down events
        /// </summary>
        /// <param name="eventArgs">keyboard event</param>
        protected void KeyDownHandler(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Key == "Enter" || eventArgs.Key == " " || eventArgs.Key == "Spacebar")
            {
                ToggleSubMenu();
            }
        }

    }
}
