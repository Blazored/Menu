using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazored.Menu
{
    public class MenuBuilder
    {
        private List<MenuItem> _menuItems;

        public MenuBuilder()
        {
            _menuItems = new List<MenuItem>();
        }
        private string CssToClose;
        private string CssToOpen;

        public MenuBuilder AddIconCssToClose(string closeCss)
        {
            CssToClose = closeCss;
            return this;
        }
        public MenuBuilder AddIconCssToOpen(string openCss) 
        {
            CssToOpen = openCss;
            return this;
        }
        public MenuBuilder AddItem(int position,
                                   string title,
                                   string link,
                                   NavLinkMatch match = NavLinkMatch.Prefix,
                                   bool isVisible = true,
                                   bool isEnabled = true)
        {
            var menuItem = new MenuItem
            {
                Position = position,
                Title = title,
                Link = link,
                Match = match,
                IsSubMenu = false,
                IsVisible = isVisible,
                IsEnabled = isEnabled
            };

            _menuItems.Add(menuItem);

            return this;
        }

        public MenuBuilder AddSubMenu(int position,
                                      string title,
                                      MenuBuilder menuItems,
                                      bool isVisible = true,
                                      bool isEnabled = true)
        {
            var menuItem = new MenuItem();
            menuItem.Position = position;
            menuItem.IsSubMenu = true;
            menuItem.Title = title;
            menuItem.MenuItems = menuItems;
            menuItem.IsVisible = isVisible;
            menuItem.IsEnabled = isEnabled;
            menuItem.IconCssToClose = CssToClose;
            menuItem.IconCssToOpen = CssToOpen;

            _menuItems.Add(menuItem);
            return this;
        }

        internal List<MenuItem> Build(Func<MenuItem, int> orderBy)
        {
            var menuItems = _menuItems.OrderBy(orderBy).ToList();
            menuItems.ForEach(m =>
            {
                m.IconCssToClose = CssToClose;
                m.IconCssToOpen = CssToOpen;
            });
            return menuItems.ToList();
        }
    }

    public class MenuItem
    {
        public int Position { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public NavLinkMatch Match { get; set; }
        public MenuBuilder MenuItems { get; set; }
        public bool IsSubMenu { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public string IconCssToOpen { get; set; }
        public string IconCssToClose { get; set; } 
    }
}
