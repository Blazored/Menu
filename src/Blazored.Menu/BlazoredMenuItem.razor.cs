using Microsoft.AspNetCore.Components;

namespace Blazored.Menu
{
    public class BlazoredMenuItemBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool IsEnabled { get; set; } = true;
        [Parameter] public bool IsVisible { get; set; } = true;
        [Parameter] public string Css { get; set; } = string.Empty;
        [Parameter] public MenuItem MenuItem { get; set; }

        protected string CssString
        {
            get
            {
                var cssString = string.Empty;

                cssString += $"{Css}";
                cssString += !IsEnabled ? " disabled" : "";
                cssString += !IsVisible ? " hidden" : "";

                return cssString.Trim();
            }
        }
    }
}
