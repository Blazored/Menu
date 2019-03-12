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
                throw new InvalidOperationException($"Cannot use child content and menu builder together");
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
                if (item.IsSubMenu && item.IsVisible)
                {
                    index = BuildSubMenu(builder, index, item);
                }
                else if (!item.IsSubMenu && item.IsVisible)
                {
                    index = BuildMenuItem(builder, index, item);
                }
            }

            builder.CloseElement();
            builder.CloseElement();
        }

        private int BuildSubMenu(RenderTreeBuilder subMenuBuilder, int index, MenuItem item)
        {
            subMenuBuilder.OpenComponent<BlazoredSubMenu>(index++);
            subMenuBuilder.AddAttribute(index++, "Header", item.Title);

            if (!item.IsEnabled)
            {
                subMenuBuilder.AddAttribute(index++, "disabled", "true");
            }

            subMenuBuilder.AddAttribute(index++, "ChildContent", (RenderFragment)((subMenuContentBuilder) =>
            {
                var subMenuItems = item.MenuItems.Build(x => x.Position);

                foreach (var subMenuItem in subMenuItems)
                {
                    index = BuildMenuItem(subMenuContentBuilder, index, subMenuItem);
                }
            }));

            subMenuBuilder.CloseComponent();

            return index;
        }

        private int BuildMenuItem(RenderTreeBuilder menuItemBuilder, int index, MenuItem item)
        {
            menuItemBuilder.OpenComponent<BlazoredMenuItem>(index++);

            if (item.IsVisible)
            {
                if (item.IsEnabled)
                {
                    menuItemBuilder.AddAttribute(index++, "ChildContent", (RenderFragment)((menuItemContentBuilder) =>
                    {
                        menuItemContentBuilder.OpenComponent<NavLink>(index++);
                        menuItemContentBuilder.AddAttribute(index++, "href", item.Link);

                        if (item.Link?.Trim() == "/")
                        {
                            menuItemContentBuilder.AddAttribute(index++, "Match", NavLinkMatch.All);
                        }

                        menuItemContentBuilder.AddAttribute(index++, "ChildContent", (RenderFragment)((navLinkContentBuilder) =>
                        {
                            navLinkContentBuilder.AddContent(index++, item.Title);
                        }));

                        menuItemContentBuilder.CloseComponent();
                    }));
                }
                else
                {
                    menuItemBuilder.AddAttribute(index++, "IsEnabled", item.IsEnabled);
                    menuItemBuilder.AddAttribute(index++, "ChildContent", (RenderFragment)((menuItemContentBuilder) =>
                    {
                        menuItemContentBuilder.AddContent(index++, item.Title);
                    }));
                }
            }
            else
            {
                menuItemBuilder.AddAttribute(index++, "IsVisible", item.IsVisible);
                menuItemBuilder.AddAttribute(index++, "ChildContent", (RenderFragment)((menuItemContentBuilder) =>
                {
                    menuItemContentBuilder.AddContent(index++, item.Title);
                }));
            }

            menuItemBuilder.CloseComponent();

            return index;
        } 
    }
}
