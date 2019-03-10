using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Blazored.Menu
{
    public class BlazoredMenu : ComponentBase
    {
        [Parameter] RenderFragment ChildContent { get; set; }
        [Parameter] MenuBuilder MenuBuilder { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ChildContent != null && MenuBuilder != null)
            {
                throw new InvalidOperationException($"Cannot use child content and Menu builder together");
            }

            if (ChildContent != null)
            {
                BuildMenuFromMarkup(builder);
            }
            else if (MenuBuilder != null)
            {
                BuildMenuFromBuilder(builder);
            }
        }

        private void BuildMenuFromBuilder(RenderTreeBuilder builder)
        {
            var index = -1;
            var menuItems = MenuBuilder.Build(x => x.Position);

            builder.OpenElement(index++, "nav");
            builder.AddAttribute(index++, "role", "navigation");
            builder.AddAttribute(index++, "class", "blazored-menu-container");
            builder.OpenElement(index++, "ul");
            builder.AddAttribute(index++, "class", "blazored-menu");

            foreach (var item in menuItems)
            {
                if (item.IsSubMenu)
                {
                    index = BuildSubMenu(builder, index, item);
                }
                else
                {
                    index = BuildMenuItem(builder, index, item);
                }
            }

            builder.CloseElement();
            builder.CloseElement();
        }

        private static int BuildMenuItem(RenderTreeBuilder builder, int index, MenuItem item)
        {
            builder.OpenElement(index++, "li");
            builder.OpenComponent<NavLink>(index++);
            builder.AddAttribute(index++, "href", item.Link);

            if (item.Link?.Trim() == "/")
            {
                builder.AddAttribute(index++, "Match", NavLinkMatch.All);
            }

            builder.AddAttribute(index++, "ChildContent", (RenderFragment)((builder2) =>
            {
                builder2.AddContent(index++, item.Title);
            }));

            builder.CloseComponent();
            builder.CloseElement();

            return index;
        }

        private static int BuildSubMenu(RenderTreeBuilder builder, int index, MenuItem item)
        {
            builder.OpenComponent<BlazoredSubMenu>(index++);
            builder.AddAttribute(index++, "Header", item.Title);
            builder.AddAttribute(index++, "ChildContent", (RenderFragment)((builder2) =>
            {
                var subMenuItems = item.MenuItems.Build(x => x.Position);
                Console.WriteLine(subMenuItems.Count);

                foreach (var subMenuItem in subMenuItems)
                {
                    builder2.OpenElement(index++, "li");
                    builder2.OpenComponent<NavLink>(index++);
                    builder2.AddAttribute(index++, "href", subMenuItem.Link);

                    if (subMenuItem.Link?.Trim() == "/")
                    {
                        builder2.AddAttribute(index++, "Match", NavLinkMatch.All);
                    }

                    builder2.AddAttribute(index++, "ChildContent", (RenderFragment)((builder3) =>
                    {
                        builder3.AddContent(index++, subMenuItem.Title);
                    }));

                    builder2.CloseComponent();
                    builder2.CloseElement();
                }
            }));

            builder.CloseComponent();

            return index;
        }

        private void BuildMenuFromMarkup(RenderTreeBuilder builder)
        {
            var index = -1;

            builder.OpenElement(index++, "nav");
            builder.AddAttribute(index++, "role", "navigation");
            builder.AddAttribute(index++, "class", "blazored-menu-container");
            builder.OpenElement(index++, "ul");
            builder.AddAttribute(index++, "class", "blazored-menu");
            builder.AddContent(index++, ChildContent);
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}
