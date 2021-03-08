using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace Blazored.Menu
{
    public class BlazoredSubMenuBase : ComponentBase
    {
        [Parameter] public string Header { get; set; }
        [Parameter] public string IconClassToClose { get; set; }
        [Parameter] public string IconClassToOpen { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment HeaderTemplate { get; set; }
        [Parameter] public RenderFragment MenuTemplate { get; set; }
        [Parameter] public string Css { get; set; }
        [Parameter] public bool IsEnabled { get; set; } = true;
        [Parameter] public IEnumerable<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        [CascadingParameter] public GlobalIconCss GlobalIconCss { get; set; }
        protected string Icon { get; set; } = "+";
        protected bool IsOpen { get; set; }
        protected string IconCss { get; set; }

        private GlobalIconCss LocalIconCss { get; set; } = new GlobalIconCss();

        protected bool SetUpDownIcon { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (string.IsNullOrEmpty(IconClassToClose) && string.IsNullOrEmpty(IconClassToOpen))
            {
                LocalIconCss = GlobalIconCss; // Takeover global definition
            }
            else
            {
                // Include local specified values
                LocalIconCss.IconClassToClose = IconClassToClose;
                LocalIconCss.IconClassToOpen = IconClassToOpen;
            }
            // Check if both are specified
            var isOpenAvailable = !string.IsNullOrEmpty(LocalIconCss.IconClassToOpen);
            var isCloseAvailable = !string.IsNullOrEmpty(LocalIconCss.IconClassToClose);
            if (!(isOpenAvailable && isCloseAvailable))
            {
                throw new ArgumentException($"{nameof(IconClassToClose)} or {nameof(IconClassToOpen)} not specified");
            }
            // If both are specified (We reach this if either none or both are specified either globally or at submenu
            if (isOpenAvailable && isCloseAvailable)
            {
                Icon = "";
                IconCss = LocalIconCss.IconClassToOpen;
                SetUpDownIcon = true;
            }


        }
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
            if (SetUpDownIcon)
            {
                IconCss = IsOpen ? LocalIconCss.IconClassToClose: LocalIconCss.IconClassToOpen;
            }
            else
            {
                Icon = IsOpen ? "-" : "+";
            }
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
