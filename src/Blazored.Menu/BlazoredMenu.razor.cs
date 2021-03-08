using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Menu
{
    public class BlazoredMenuBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public MenuBuilder MenuBuilder { get; set; }
        [Parameter] public string Css { get; set; }
        [Parameter] public string IconClassToClose { get; set; }
        [Parameter] public string IconClassToOpen { get; set; }
        protected GlobalIconCss IconCss { get; set; } = new GlobalIconCss();
        protected override void OnParametersSet()
        {
            if (ChildContent != null && MenuBuilder != null)
            {
                throw new InvalidOperationException($"Cannot use child content and menu builder together");
            }
            var isOpenAvailable = string.IsNullOrEmpty(IconClassToOpen);
            var isCloseAvailble = string.IsNullOrEmpty(IconClassToClose);
            if (isOpenAvailable ^ isCloseAvailble)
            {
                throw new ArgumentException($"{nameof(IconClassToClose)} or {nameof(IconClassToOpen)} not specified");
            }

            IconCss.IconClassToClose = IconClassToClose;
            IconCss.IconClassToOpen = IconClassToOpen;
        }
    }
}
