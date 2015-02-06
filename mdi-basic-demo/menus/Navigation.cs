using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.menus.managers;
using mdi_api.views;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus {
    class Navigation : Menu {

        private readonly Navigator _navigator;
        private readonly Item _navigate;

        public Navigation() : base("Navigation") {
            _navigator = new Navigator(this);

            View content = Get(ViewFactory.CONTENT);
            content.VerticalSpacing = 3;

            _navigate = new Item("Navigate");
            _navigate.AddHotKeyListener(ConsoleKey.N, Navigate);
            content.Add(_navigate);

            const string infoText = "If your Menu class has other menus it would like to bring focus to, you can use a \"Navigator\". In this case " +
                "we have an item titled Navigate that when pressed, will tell the navigator to lock this menu and focus on a new instance of this menu instead. " +
                "And we get a hierarchy of menus. " +
                "~ ~" +
                "One thing to note is that when you use a navigator to bring a new menu into focus, is that you can pass an identifier argument and decide what " +
                "item in the newely created menu should be used as a \"Back\" item. If none is specified, the navigator will look for the footer view, and automatically " +
                "create a Back item for you. And since the Main/root menu does not specify any, we get the back item the first time we enter this menu.";

            Item info = new Item("Navigator", Item.Alignment.CENTER, "Info", infoText){Separator = new Separator(content.GetHorizontalSpace()-12)};

            content.AddAll(new Item("Head Back"), info);
        }

        public void Navigate() {
            _navigator.Focus(new Navigation(), "Head Back");
        }

        public override void Dispose() {
            _navigator.Release();
            _navigate.RemoveHotKeyListener(Navigate);
        }
    }
}
