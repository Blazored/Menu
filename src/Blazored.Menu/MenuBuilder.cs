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

        public MenuBuilder AddItem(int position, string title, string link, bool IsVisible = true, bool IsEnabled = true)
        {
            var menuItem = new MenuItem();
            menuItem.Position = position;
            menuItem.Title = title;
            menuItem.Link = link;
            menuItem.IsSubMenu = false;
            menuItem.IsVisible = IsVisible;
            menuItem.IsEnabled = IsEnabled;

            _menuItems.Add(menuItem);

            return this;
        }

        public MenuBuilder AddSubMenu(int position, string title, MenuBuilder menuItems, bool IsVisible = true, bool IsEnabled = true)
        {
            var menuItem = new MenuItem();
            menuItem.Position = position;
            menuItem.IsSubMenu = true;
            menuItem.Title = title;
            menuItem.MenuItems = menuItems;
            menuItem.IsVisible = IsVisible;
            menuItem.IsEnabled = IsEnabled;

            _menuItems.Add(menuItem);
            return this;
        }

        internal List<MenuItem> Build(Func<MenuItem, int> orderBy)
        {
            var menuItems = _menuItems.OrderBy(orderBy);

            return menuItems.ToList();
        }
    }

    public class MenuItem
    {
        public int Position { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public MenuBuilder MenuItems { get; set; }
        public bool IsSubMenu { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
    }
}
