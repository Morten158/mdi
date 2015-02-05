using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.menus.managers;
using mdi_api.utils;
using mdi_api.views;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus {

    class Main : Menu {

        private readonly Item _menus, _views, _items, _navigation;
        private readonly Navigator _navigator;

        public Main() : base("Welcome to the basic demo application of the mdi api.") {

            _navigator = new Navigator(this);
            View content = Get(ViewFactory.CONTENT);
            content.VerticalSpacing = 2;

            _menus = new Item("Menus", Item.Alignment.CENTER, "Nr.1", "See how the menus work.");
            _menus.Separator = new Separator(content.GetHorizontalSpace()/2);
            _menus.AddHotKeyListener(ConsoleKey.M, EnterMenus);

            _views = new Item("Views", Item.Alignment.CENTER, "Nr.2", "See how the views work.");
            _views.Separator = new Separator(content.GetHorizontalSpace()/2);
            _views.AddHotKeyListener(ConsoleKey.V, EnterViews);

            _items = new Item("Items", Item.Alignment.CENTER, "Nr.3", "See how the items work.");
            _items.Separator = new Separator(content.GetHorizontalSpace()/2);
            _items.AddHotKeyListener(ConsoleKey.I, EnterItems);

            _navigation = new Item("Navigation", Item.Alignment.CENTER, "Nr.4", "How to navigate trough menus.");
            _navigation.Separator = new Separator(content.GetHorizontalSpace()/2);
            _navigation.AddHotKeyListener(ConsoleKey.N, EnterNavigation);
            
            content.AddAll(_menus, _views, _items, _navigation);
        }

        public void EnterMenus() {_navigator.Focus(new Menus());}
        public void EnterViews() {_navigator.Focus(new Views());}
        public void EnterItems() {_navigator.Focus(new Items());}
        public void EnterNavigation() {_navigator.Focus(new Navigation());}

        public override void Dispose() {
            _navigator.Release();
            _menus.RemoveHotKeyListener(EnterMenus);
            _menus.RemoveHotKeyListener(EnterMenus);
            _menus.RemoveHotKeyListener(EnterMenus);
            _menus.RemoveHotKeyListener(EnterMenus);
        }
    }
}
