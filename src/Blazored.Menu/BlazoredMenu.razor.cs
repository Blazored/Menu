using Microsoft.AspNetCore.Components;

namespace Blazored.Menu
{
    public class BlazoredMenuBase : ComponentBase
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
