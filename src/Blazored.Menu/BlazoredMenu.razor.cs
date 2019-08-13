using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Menu
{
    public class BlazoredMenuBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public MenuBuilder MenuBuilder { get; set; }
        [Parameter] public string Css { get; set; }

        protected override void OnParametersSet()
        {
            if (ChildContent != null && MenuBuilder != null)
            {
                throw new InvalidOperationException($"Cannot use child content and menu builder together");
            }
        }
    }
}
