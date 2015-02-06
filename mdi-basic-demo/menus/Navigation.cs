using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.menus.managers;
using mdi_api.views;
using mdi_api.views.items;

namespace mdi_basic_demo.menus {
    class Navigation : Menu {

        private readonly Navigator _navigator;
        private readonly Item _navigate;

        public Navigation() : base("Navigation") {
            _navigator = new Navigator(this);

            View content = Get(ViewFactory.CONTENT);
            content.VerticalSpacing = 4;

            _navigate = new Item("Navigate");
            _navigate.AddHotKeyListener(ConsoleKey.N, Navigate);
            content.Add(_navigate);

            content.Add(new Item("Head Back"));

            const string infoText = "A \"Navigator\" ";
            Item info = new Item("Navigator", Item.Alignment.CENTER, "Info", infoText);
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
