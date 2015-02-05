using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_snake.menus {
    class Themes : Menu {

        private readonly Item _standard, _sharpBurgundy, _contrast, _custom1;

        public Themes()
            : base("Themes", Application.Theme) {

            View content = Get(ViewFactory.CONTENT);
            content.Margin = new Margin(8, 8, 2, 2);
            content.VerticalSpacing = 1;
            
            _standard = new Item(Theme.DEFAULT) {Separator = new Separator(content.GetHorizontalSpace())};
            _standard.AddHotKeyListener(ConsoleKey.D, Default);

            _sharpBurgundy = new Item(Theme.SHARP_BURGUNDY) {Separator = new Separator(content.GetHorizontalSpace())};
            _sharpBurgundy.AddHotKeyListener(ConsoleKey.S, SharpBurgundy);

            _contrast = new Item(Theme.CONTRAST) {Separator = new Separator(content.GetHorizontalSpace())};
            _contrast.AddHotKeyListener(ConsoleKey.C, Contrast);

            _custom1 = new Item(Theme.CUSTOM_ONE) {Separator = new Separator(content.GetHorizontalSpace())};
            _custom1.AddHotKeyListener(ConsoleKey.O, CustomOne);

            content.AddAll(_standard, _sharpBurgundy, _contrast, _custom1);
        }

        public void Default() {_ApplyTheme(Theme.DEFAULT);}
        public void SharpBurgundy() {_ApplyTheme(Theme.SHARP_BURGUNDY);}
        public void Contrast() {_ApplyTheme(Theme.CONTRAST);}
        public void CustomOne() {_ApplyTheme(Theme.CUSTOM_ONE);}

        public void _ApplyTheme(Theme theme) {
            Application.Theme = theme;
            ApplyTheme(theme, true);
            Application.ApplyTheme(theme);
        }

        public override void Dispose() {
            _standard.RemoveHotKeyListener(Default);
            _sharpBurgundy.RemoveHotKeyListener(SharpBurgundy);
            _contrast.RemoveHotKeyListener(Contrast);
            _custom1.RemoveHotKeyListener(CustomOne);
        }
    }
}
