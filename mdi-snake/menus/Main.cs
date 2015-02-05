using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.menus.managers;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_snake.menus {
    class Main : Menu {

        private readonly Item _snake, _themes, _resolutions, _about;
        private readonly Navigator _navigator;

        public Main()
            : base("Main Menu", Application.Theme) {

            _navigator = new Navigator(this);
            const int seperatorLength = 14;

            View content = Get(ViewFactory.CONTENT);
            content.Margin = new Margin(5, 5, 5, 5);
            content.VerticalSpacing = 2;

            _snake = new Item("Snake") {Separator = new Separator(seperatorLength)};
            _snake.AddHotKeyListener(ConsoleKey.S, EnterSnake);

            _themes = new Item("Themes") {Separator = new Separator(seperatorLength)};
            _themes.AddHotKeyListener(ConsoleKey.T, EnterThemes);

            _resolutions = new Item("Resolutions") {Separator = new Separator(seperatorLength)};
            _resolutions.AddHotKeyListener(ConsoleKey.R, EnterResolutions);

            _about = new Item("About") {Separator = new Separator(seperatorLength)};
            _about.AddHotKeyListener(ConsoleKey.A, EnterAbout);

            content.AddAll(_snake, _themes, _resolutions, _about);
        }

        public void EnterSnake() {_navigator.Focus(new Snake());}
        public void EnterThemes() {_navigator.Focus(new Themes());}
        public void EnterResolutions() {_navigator.Focus(new Resolutions());}
        public void EnterAbout() {_navigator.Focus(new AboutSnake());}

        public override void Dispose() {
            _navigator.Release();
            _snake.RemoveHotKeyListener(EnterSnake);
            _themes.RemoveHotKeyListener(EnterResolutions);
            _resolutions.RemoveHotKeyListener(EnterResolutions);
            _about.RemoveHotKeyListener(EnterAbout);
        }
    }
}
