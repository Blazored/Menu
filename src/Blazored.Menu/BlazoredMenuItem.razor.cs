using Microsoft.AspNetCore.Components;

namespace Blazored.Menu
{
    public class BlazoredMenuItemBase : ComponentBase
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool IsEnabled { get; set; } = true;
        [Parameter] protected bool IsVisible { get; set; } = true;
        [Parameter] protected string Css { get; set; } = string.Empty;
        [Parameter] protected MenuItem MenuItem { get; set; }

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
